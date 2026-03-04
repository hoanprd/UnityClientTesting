using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField user, pass;
    public Button login, registry;

    public GameObject[] registryPart, loginPart;

    // Start is called before the first frame update
    void Start()
    {
        login.onClick.AddListener(() =>
        {
            Main.Instance.dataController.Login(user.text, pass.text);
        });

        registry.onClick.AddListener(() =>
        {
            foreach (var part in registryPart)
            {
                part.SetActive(true);
            }

            foreach (var part in loginPart)
            {
                part.SetActive(false);
            }
        });
    }
}
