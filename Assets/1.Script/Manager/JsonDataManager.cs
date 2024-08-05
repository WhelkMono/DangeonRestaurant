using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public int id;
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
    public int id;
    public string name;
    public string description;
    public int price;
    public string[] recipe;
}

[System.Serializable]
public class IngredientData
{
    public int id;
    public string name;
    public string description;
    public int price;
}

[System.Serializable]
public class ItemJsonData
{
    public List<FoodData> foodDatas;
    public List<IngredientData> ingredientDatas;

    public ItemJsonData()
    {
        foodDatas = new List<FoodData>();
        ingredientDatas = new List<IngredientData>();
    }
}

public enum ItemDataType
{
    foodData,
    ingredientData,
    gadget
}

[System.Serializable]
public class ItemData
{
    public ItemDataType type;
    public int id;
    public int count;
}

[System.Serializable]
public class InventoryData
{
    public int invenCount;
    public List<ItemData> itemDatas;

    public InventoryData()
    {
        invenCount = 10;
        itemDatas = new();

        /*//Test
        ItemData itemData = new();
        itemData.type = ItemDataType.ingredientDat;
        itemData.id = 1;
        itemData.count = 3;
        itemDatas = new();
        itemDatas.Add(itemData);*/
    }
}

[System.Serializable]
public class PlayerData
{
    public int HP;
    public int Hunger;
    public int Coin;

    public PlayerData()
    {
        HP = 100;
        Hunger = 100;
        Coin = 0;
    }
}

[System.Serializable]
public class StorageData
{
    public PlayerData playerData;
    public InventoryData playerInven;
    public InventoryData restaurantBoxInven;

    public StorageData()
    {
        playerData = new PlayerData();
        playerInven = new InventoryData();
        restaurantBoxInven = new InventoryData();
    }
}

public class JsonDataManager : Singleton<JsonDataManager>
{
    private string filePath;
    public StorageData storageData;
    public ObjectData objectData;
    public ItemJsonData itemData;

    void Awake()
    {
        DontDestroyOnLoad(this);

        ResetPlayerJsonData();
        //SavePlayerJsonData();
        LoadPlayerJsonData();
        LoadItemJsonData();
        LoadObjectJsonData();
    }

    private void ResetPlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string jsonData = JsonUtility.ToJson(new StorageData());
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    private void SavePlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string jsonData = JsonUtility.ToJson(storageData);
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    private void LoadPlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        storageData = JsonUtility.FromJson<StorageData>(loadedJsonData);
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
        itemData = JsonUtility.FromJson<ItemJsonData>(loadedJsonData);
    }
}
