using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServerConnection
{
    IEnumerator Get(string uri, Action<string> onSuccess, Action<string> onError);
    IEnumerator Post(string uri, Dictionary<string, string> formData, Action<string> onSuccess, Action<string> onError);
}
