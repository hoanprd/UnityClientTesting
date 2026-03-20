using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

    string _basePath;
    string _versionJsonPath;
    JSONObject _versionJson;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        _basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }

        _versionJson = new JSONObject();
        _versionJsonPath = _basePath + "VersionJson";

        if (File.Exists(_versionJsonPath))
        {
            string jsonString = File.ReadAllText(_versionJsonPath);
            _versionJson = JSON.Parse(jsonString) as JSONObject;
        }
    }

    bool ImageExists(string imageName)
    {
        return File.Exists(_basePath + imageName);
    }

    public void SaveImage(string imageName, byte[] imageBytes, int imgVer)
    {
        File.WriteAllBytes(_basePath + imageName, imageBytes);
        UpdateVersionJson(imageName, imgVer);
    }

    public byte[] LoadImage(string imageName, int imgVer)
    {
        byte[] imageBytes = new byte[0];

        if (!IsImageUpToDate(imageName, imgVer))
        {
            return imageBytes;
        }

        if (ImageExists(imageName))
        {
            imageBytes = File.ReadAllBytes(_basePath + imageName);
        }
        return imageBytes;
    }

    public Sprite BytesToSprite(byte[] bytes)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    
        return sprite;
    }

    void UpdateVersionJson(string name, int ver)
    {
        _versionJson[name] = ver;
    }

    bool IsImageUpToDate(string name, int ver)
    {
        if (_versionJson != null)
        {
            return _versionJson[name].AsInt == ver;
        }
        else
        {
            return false;
        }
    }

    public void SaveVersionJson()
    {
        File.WriteAllText(_versionJsonPath, _versionJson.ToString());
    }
}
