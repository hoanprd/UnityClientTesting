using System.Runtime.CompilerServices;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string userID { get; private set; }
    string userName;
    string userPassword;
    string level;
    string Coins;

    public void SetCredentials(string username, string userpassword)
    {
        userName = username;
        userPassword = userpassword;
    }

    public void SetID(string id)
    {
        userID = id;
    }
}
