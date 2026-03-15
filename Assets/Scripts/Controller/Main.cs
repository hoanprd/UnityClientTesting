using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public DataController dataController;
    public UserInfo userInfo;

    public GameObject userProfile, userInvetory, loginRegistryUI;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        dataController = GetComponent<DataController>();
        userInfo = GetComponent<UserInfo>();
    }
}
