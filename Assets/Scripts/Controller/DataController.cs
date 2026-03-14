using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class DataController : MonoBehaviour
{
    public string urlDate, urlGetUser, urlLogin, urlRegistry;

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

        /*StartCoroutine(_webService.Get(
            urlGetUser,
            (data) => Debug.Log(data),
            (error) => Debug.LogError("Lỗi: " + error)
        ));*/

        /*StartCoroutine(_webService.Post(
            urlLogin,
            data,
            (response) => {
                Debug.Log("Server phản hồi: " + response);
                // Xử lý logic đăng nhập thành công ở đây (chuyển scene, lưu token...)
            },
            (error) => {
                Debug.LogError("Đăng nhập thất bại: " + error);
            }
        ));*/
    }

    public void Login(string username, string password)
    {
        var data = new Dictionary<string, string>
        {
            { "loginUser", username },
            { "loginPass", password }
        };
        StartCoroutine(_webService.Post(
            urlLogin,
            data,
            (response) => {
                Debug.Log("Server phản hồi: " + response);
                if (response != "Unvalid password" && response != "Unvalid user")
                {
                    // Xử lý logic đăng nhập thành công ở đây (chuyển scene, lưu token...)
                    Main.Instance.loginRegistryUI.SetActive(false);
                    Main.Instance.userProfile.SetActive(true);
                    Main.Instance.userInfo.SetCredentials(username, password);
                    Main.Instance.userInfo.SetID(response);
                    Main.Instance.dataController.StartCoroutine(Main.Instance.dataController.GetItemIDs(Main.Instance.userInfo.userID));
                }
            },
            (error) => {
                Debug.LogError("Đăng nhập thất bại: " + error);
            }
        ));
    }

    public void Registry(string username, string password)
    {
        var data = new Dictionary<string, string>
        {
            { "loginUser", username },
            { "loginPass", password }
        };
        StartCoroutine(_webService.Post(
            urlRegistry,
            data,
            (response) => {
                Debug.Log("Server phản hồi: " + response);
                // Xử lý logic đăng nhập thành công ở đây (chuyển scene, lưu token...)
            },
            (error) => {
                Debug.LogError("Đăng nhập thất bại: " + error);
            }
        ));
    }

    IEnumerator GetItemIDs(string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/GameServer/GetItemsIDs.php", form))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                // Xử lý logic đăng nhập thành công ở đây (chuyển scene, lưu token...)
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
            }
        }
    }
}
