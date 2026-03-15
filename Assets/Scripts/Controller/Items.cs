using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class Items : MonoBehaviour
{
    Action<string> _createItemCallback;

    void Start()
    {
        _createItemCallback = (jsonArray) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArray));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string _userId = Main.Instance.userInfo.userID;
        StartCoroutine(Main.Instance.dataController.GetItemIDs(_userId, _createItemCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isDone = false;
            string itemId = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.dataController.GetItem(itemId, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            item.transform.Find("NameText (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["name"];
            item.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["price"];
            item.transform.Find("DescriptionText (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["description"];
        }
    }
}
