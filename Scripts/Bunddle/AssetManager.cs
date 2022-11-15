using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AssetManager : MonoBehaviour
{

    public static AssetManager instance;

    private void Awake()
    {
        AssetManager.instance = this;


    }
    public IEnumerator LoadFromMemoryAsynce(string path, System.Action<AssetBundle> callback)
    {
        //������ ����Ʈ �迭�� �о �񵿱� ��ķ� �ε��Ѵ�.

        byte[] binary = File.ReadAllBytes(path);
        AssetBundleCreateRequest req =
        AssetBundle.LoadFromMemoryAsync(binary);

        yield return req;

        callback(req.assetBundle);

    }

}