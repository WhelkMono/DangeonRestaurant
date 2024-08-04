using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public string name;
    public string[] desc;
}

[System.Serializable]
public class ObjectData
{
    public NPCData[] npcDatas;
}

[System.Serializable]
public class FoodData
{
    public string name;
    public string description;
    public int price;
    public string[] recipe;
}

[System.Serializable]
public class IngredientData
{
    public string name;
    public string description;
    public int price;
}

[System.Serializable]
public class ItemData
{
    public List<FoodData> foodDatas;
    public List<IngredientData> ingredientDatas;

    public ItemData()
    {
        foodDatas = new List<FoodData>();
        ingredientDatas = new List<IngredientData>();
    }
}

[System.Serializable]
public class ItemAddressData
{
    public ItemDataType type;
    public int id;
    public int count;
}

[System.Serializable]
public class PlayerData
{
    public List<ItemAddressData> inventoryData;
    public List<ItemAddressData> resttaurantBoxData;
}

public class JsonDataManager : Singleton<JsonDataManager>
{
    private string filePath;
    public PlayerData playerData;
    public ItemData itemData;
    public ObjectData objectData;

    void Awake()
    {
        DontDestroyOnLoad(this);

        //ResetPlayerJsonData();
        //SavePlayerJsonData();
        LoadPlayerJsonData();
        LoadItemJsonData();
        LoadObjectJsonData();
    }

    private void ResetPlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        PlayerData data = new PlayerData();
        data.inventoryData = new List<ItemAddressData>();
        data.resttaurantBoxData = new List<ItemAddressData>();

        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    private void SavePlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    private void LoadPlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        playerData = JsonUtility.FromJson<PlayerData>(loadedJsonData);
    }

    private void LoadObjectJsonData()
    {
        filePath = "Assets/7.Data/ObjectData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        objectData = JsonUtility.FromJson<ObjectData>(loadedJsonData);
    }

    private void LoadItemJsonData()
    {
        filePath = "Assets/7.Data/ItemData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        itemData = JsonUtility.FromJson<ItemData>(loadedJsonData);
    }
}
