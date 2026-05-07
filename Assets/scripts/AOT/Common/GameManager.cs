using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public MeshRenderer plane;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.Space(20);
        GUILayout.Space(20);

        if (GUILayout.Button("写入p目录一个文件", GUILayout.Width(200), GUILayout.Height(50)))
        {

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            byte[] datas = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "E_Tex_wq_sj_403.png"));
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "ts.txt"), datas.Length.ToString());
#elif UNITY_ANDROID

            StartCoroutine(LoadTextureAsync("E_Tex_wq_sj_403.png", (tex) =>
        {
            //plane.material.mainTexture = tex;
            plane.material.SetTexture("_MainTex", tex);
            plane.material.color = Color.red;
        }));

#endif
        }


        if (GUILayout.Button("调用Android SDK", GUILayout.Width(200), GUILayout.Height(50)))
        {

#if UNITY_ANDROID

            using (var mathUtil = new AndroidJavaClass("MathUtil"))
            {
                float result = mathUtil.CallStatic<float>("add", 1.5f, 2.3f);
                Debug.Log($"调用 Android SDK 结果: {result}");
            }

#endif

        }
    }

    /// <summary>
    /// 协程方式加载 StreamingAssets 中的图片为 Texture2D
    /// </summary>
    /// <param name="fileName">文件名（含扩展名，例如 "myImage.png"）</param>
    /// <param name="onComplete">加载完成回调，参数为 Texture2D（失败时为 null）</param>
    /// <returns>协程迭代器</returns>
    public IEnumerator LoadTextureAsync(string fileName, System.Action<Texture2D> onComplete)
    {
        // 拼接完整路径（Application.streamingAssetsPath 自动适配 Android）
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        ///jar:file:///data/app/com.yourcompany.yourgame-1/base.apk!/assets/E_Tex_wq_sj_403.png
        
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(fullPath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                // 从下载处理器获取 Texture2D
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                onComplete?.Invoke(texture);
                Debug.Log($"加载图片成功: {fileName}");
            }
            else
            {
                Debug.LogError($"加载图片失败: {fileName}, 错误: {uwr.error}");
                onComplete?.Invoke(null);
            }
        }
    }
 



}
