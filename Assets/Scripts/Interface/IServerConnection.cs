using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServerConnection
{
    IEnumerator Get(string uri, Action<string> onSuccess, Action<string> onError);
}
