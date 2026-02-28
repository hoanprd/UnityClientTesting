using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public string urlDate;

    private IServerConnection _webService;

    // Start is called before the first frame update
    void Start()
    {
        _webService = new ServerConnection();

        StartCoroutine(_webService.Get(
            urlDate,
            (data) => Debug.Log(data),
            (error) => Debug.LogError("Lỗi: " + error)
        ));
    }
}
