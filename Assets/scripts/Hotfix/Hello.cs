using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello
{
    public static void Run()
    {
        Debug.Log("你好啊, HybridCLR");

        List<float> floats = new List<float>();

        floats.Add(1.0f);
        Debug.Log(floats[0] + "  count: " + floats.Count);

        Dictionary<int, string> dict = new Dictionary<int, string>();

        dict.Add(1, "hello");
        Debug.Log(dict[1] + "dict count111111111111111 = " + dict.Count);

        ///调用aot中的UIManager,如果能正确调用，说明热更新dll成功调用了aot dll中的代码
        UIManager uIManager = new UIManager();
        uIManager.ShowPanel("商城");
    }
}