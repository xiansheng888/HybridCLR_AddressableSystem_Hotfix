using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadDll : MonoBehaviour
{
    IEnumerator Start()
    {

        // 初始化Addressables
        var initHandle = Addressables.InitializeAsync();
        yield return initHandle;

        // 先加载元数据
        yield return StartCoroutine(LoadMetadataForAOTAssemblies());

        // Editor环境下，HotUpdate.dll已自动加载，无需从Addressables重复加载
#if !UNITY_EDITOR
        var loadHandle = Addressables.LoadAssetAsync<TextAsset>("HotUpdate.dll");
        yield return loadHandle;
        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Assembly hotUpdateAss = Assembly.Load(loadHandle.Result.bytes);
            Type type = hotUpdateAss.GetType("Hello");
            type.GetMethod("Run").Invoke(null, null);
        }
        else
        {
            Debug.LogError("加载HotUpdate.dll失败: " + loadHandle.OperationException);
        }
#else
        Assembly hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
        Type type = hotUpdateAss.GetType("Hello");
        type.GetMethod("Run").Invoke(null, null);
#endif
    }

    private IEnumerator LoadMetadataForAOTAssemblies()
    {
        List<string> aotDllList = new List<string>
        {
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll",
        };

        foreach (var aotDllName in aotDllList)
        {
            var loadHandle = Addressables.LoadAssetAsync<TextAsset>(aotDllName);
            yield return loadHandle;
            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                byte[] dllBytes = loadHandle.Result.bytes;
                LoadImageErrorCode err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
                Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. ret:{err}");
            }
            else
            {
                Debug.LogError($"加载元数据失败: {aotDllName}: {loadHandle.OperationException}");
            }
        }
    }

    void Update()
    {
    }
}
