using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerConnection : IServerConnection
{
    public IEnumerator Get(string uri, Action<string> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(webRequest.downloadHandler.text);
                    break;
                default:
                    onError?.Invoke($"{webRequest.result} - {webRequest.error}");
                    break;
            }
        }
    }
}