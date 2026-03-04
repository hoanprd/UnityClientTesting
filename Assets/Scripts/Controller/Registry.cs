using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Registry : MonoBehaviour
{
    public TMP_InputField user, pass, confirmPass;
    public Button registry, back;

    public GameObject[] registryPart, loginPart;

    void Start()
    {
        registry.onClick.AddListener(() =>
        {
            if (pass.text != confirmPass.text)
            {
                Debug.LogError("Mật khẩu xác nhận không khớp!");
                return;
            }
            else
                Main.Instance.dataController.Registry(user.text, pass.text);
        });

        back.onClick.AddListener(() =>
        {
            foreach (var part in registryPart)
            {
                part.SetActive(false);
            }

            foreach (var part in loginPart)
            {
                part.SetActive(true);
            }
        });
    }
}
