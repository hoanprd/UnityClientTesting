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

    public IEnumerator Post(string uri, Dictionary<string, string> formData, Action<string> onSuccess, Action<string> onError)
    {
        WWWForm form = new WWWForm();
        foreach (var item in formData)
        {
            form.AddField(item.Key, item.Value);
        }

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(www.downloadHandler.text);
            }
            else
            {
                onError?.Invoke($"{www.result}: {www.error}");
            }
        }
    }
}