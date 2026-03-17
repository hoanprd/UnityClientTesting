using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class ItemManager : MonoBehaviour
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
            string id = jsonArray[i].AsObject["ID"];

            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.dataController.GetItem(itemId, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject itemGO = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Item item = itemGO.AddComponent<Item>();

            item.id = id;
            item.itemID = itemId;

            itemGO.transform.SetParent(this.transform);
            itemGO.transform.localScale = Vector3.one;
            itemGO.transform.localPosition = Vector3.zero;

            itemGO.transform.Find("NameText (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["name"];
            itemGO.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["price"];
            itemGO.transform.Find("DescriptionText (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["description"];

            Action<Sprite> getItemIconCallback = (downloadedSprite) =>
            {
                itemGO.transform.Find("AvatarImage").GetComponent<Image>().sprite = downloadedSprite;
            };

            StartCoroutine(Main.Instance.dataController.GetItemIcon(itemId, getItemIconCallback));

            //Set sell button
            itemGO.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string idInInventory = id;
                string iId = itemId;
                string userId = Main.Instance.userInfo.userID;

                StartCoroutine(Main.Instance.dataController.SellItem(idInInventory, itemId, userId));
            });
        }
    }
}
