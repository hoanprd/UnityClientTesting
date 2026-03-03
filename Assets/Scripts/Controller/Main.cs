using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public DataController dataController;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        dataController = GetComponent<DataController>();
    }
}
