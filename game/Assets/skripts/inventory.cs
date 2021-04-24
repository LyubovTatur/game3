using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class inventory : MonoBehaviour
{
    public database data;
    public List<ItemInventory> items = new List<ItemInventory>();
    public GameObject gameObjectShow;// (?) 
    public GameObject InventoryMainObject;
    public int itemCount;// я изменила макс на колво простов инвентаре
    public Camera cam;
    public EventSystem es;
    public int currID;
    public ItemInventory currItem;
    //public RectTransform movingObject;
    public GameObject movingObject;
    public Vector3 offset;
    public SavingValues savingValues;

    public void Start()
    {
        
        Load();

    }
    
    public void Update()
    {
        if (currID!=-1)
        {
            MoveObject();
        }
        if (Input.GetMouseButtonDown(1) && currID!=-1)
        {
            InstnantiateGameObj();
        }
        //UpdateInventory();
    }
    public void InsertIntoInv(int id, Item item, int count)
    {

    }

    //добавляет на конкретное место конкретный предмет
    public void AddItem(int id, Item item, int count)
    {
        //ItemInventory tempItem = new ItemInventory();
        //tempItem.id = item.id;
        //tempItem.count = count;
        //tempItem.gameObject.GetComponentInChildren<Image>().sprite = item.image;// тут я тоже что то поменяла а именно срау спрайт подсоединила     
        //items[id]=tempItem;
        //if (count > 1 && item.id != 0)// почему нуль??
        //{
        //    items[id].gameObject.GetComponentInChildren<Text>().text = count.ToString();
        //}
        //else
        //{
        //    items[id].gameObject.GetComponentInChildren<Text>().text = "";
        //}

        items[id].id = item.id;
        items[id].count = count;
        items[id].gameObject.GetComponentInChildren<Image>().sprite = item.image;// тут я тоже что то поменяла а именно срау спрайт подсоединила
        if (count > 1 && item.id != 0)// почему нуль??
        {
            items[id].gameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].gameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].gameObject.GetComponentInChildren<Image>().sprite = data.items[invItem.id].image;// тут я тоже что то поменяла а именно срау спрайт подсоединила
        if (invItem.count > 1 && invItem.id != 0)// почему нуль??
        {
            items[id].gameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].gameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < itemCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;
            newItem.name = i.ToString();
            ItemInventory ii = new ItemInventory();
            ii.gameObject = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button itemButton = newItem.GetComponent<Button>();

            itemButton.onClick.AddListener(delegate
            {
                SelectObject();
            });
            items.Add(ii);
        }
    }

    public void AddGraphicsI(int i)
    {

        //GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;
        //newItem.name = i.ToString();
        //ItemInventory ii = new ItemInventory();
        //ii.gameObject = newItem;
        //RectTransform rt = newItem.GetComponent<RectTransform>();
        //rt.localPosition = new Vector3(0, 0, 0);
        //rt.localScale = new Vector3(1, 1, 1);
        //newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        //Button itemButton = newItem.GetComponent<Button>();

        //itemButton.onClick.AddListener(delegate
        //{
        //    SelectObject();
        //});
        //items.Add(ii);
        int isItemExist = -1;
        foreach (var item in items)
        {
            if (item.id==i)
            {
                isItemExist = int.Parse(item.gameObject.name);
            }
        }
        if (isItemExist != -1)
        {
            items[isItemExist].count++;
            UpdateInventory();

            //if (items[isItemExist].gameObject.GetComponentInChildren<Text>().text=="")
            //{
            //    items[isItemExist].gameObject.GetComponentInChildren<Text>().text = "2";
            //}
            //else
            //{
            //    items[isItemExist].gameObject.GetComponentInChildren<Text>().text = (int.Parse(items[isItemExist].gameObject.GetComponentInChildren<Text>().text)+1).ToString();
            //}



            //if (items[i].id == 0)
            //{
            //    //items.Remove(items[i]);
            //    //Destroy(InventoryMainObject.GetComponentsInChildren<GameObject>()[i]);
            //}
            //items[i].gameObject.GetComponentInChildren<Image>().sprite = data.items[items[i].id].image;

        }
        else
        {

            //GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;
            //newItem.name = i.ToString();
            //ItemInventory ii = new ItemInventory();
            //ii.gameObject = newItem;
            //RectTransform rt = newItem.GetComponent<RectTransform>();
            //rt.localPosition = new Vector3(0, 0, 0);
            //rt.localScale = new Vector3(1, 1, 1);
            //newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            //Button itemButton = newItem.GetComponent<Button>();

            //itemButton.onClick.AddListener(delegate
            //{
            //    SelectObject();
            //});
            //items.Add(ii);
            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;
            newItem.name = items.Count.ToString();
            ItemInventory ii = new ItemInventory();
            ii.gameObject = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button itemButton = newItem.GetComponent<Button>();

            itemButton.onClick.AddListener(delegate
            {
                SelectObject();
            });
            items.Add(ii);
            AddItem(items.Count-1, data.items[i],1);

            UpdateInventory();

        }
        

    }
    public void RemoveFromInv(int InvIndx)
    {

        
        items.Remove(items[InvIndx]);

        //Destroy(InventoryMainObject.GetComponentsInChildren<GameObject>()[0]);       
        Destroy(es.currentSelectedGameObject);
        // List<ItemInventory> tempItems = new List<ItemInventory>();

       //for (int i = 0; i < InventoryMainObject.transform.childCount; i++)
       //{
       //    Destroy(InventoryMainObject.transform.GetChild(i));
       //}

       //for (int i = 0; i < items.Count; i++)
       //{
       // AddGraphicsI(i);
       //  UpdateInventory();
       //}
    }   //

    public void SelectObject()
    {
        if (currID == -1)
        {

            currID = int.Parse(es.currentSelectedGameObject.name);
            currItem = CopyInventoryItem(items[currID]);
            print(es.currentSelectedGameObject.GetType());
            //currItem = CopyInventoryItem(items[currID]);
            //movingObject.GetComponent<Image>().sprite = data.items[currItem.id].model.GetComponent<SpriteRenderer>().sprite;
            movingObject.GetComponent<SpriteRenderer>().sprite = data.items[currItem.id].model.GetComponent<SpriteRenderer>().sprite;
            //Vector2 temp = cam.WorldToScreenPoint(data.items[currItem.id].model.GetComponent<SpriteRenderer>().size);
            //movingObject.GetComponent<RectTransform>().sizeDelta = temp;
            print(data.items[currItem.id].name);
            movingObject.GetComponent<Transform>().localScale = data.items[currItem.id].model.GetComponent<Transform>().localScale;
            //AddItem(currID, data.items[0], 0);//при перетаскивании обьекта типо заменяется плейс на ноль
            movingObject.gameObject.SetActive(true);
            if (es.currentSelectedGameObject.GetComponentInChildren<Text>().text=="")
            {
                print(items.Remove(items[currID]));
                for (int i = currID; i < items.Count; i++)
                {
                    items[i].gameObject.name = (int.Parse(items[i].gameObject.name)-1).ToString();
                }
                //RemoveFromInv(currID);
                Destroy(es.currentSelectedGameObject);
            }
            else
            {
                items[currID].count--;

                if (es.currentSelectedGameObject.GetComponentInChildren<Text>().text == "2")
                {
                    es.currentSelectedGameObject.GetComponentInChildren<Text>().text = "";
                }
                else
                {
                    es.currentSelectedGameObject.GetComponentInChildren<Text>().text = (int.Parse(es.currentSelectedGameObject.GetComponentInChildren<Text>().text)-1).ToString();
                }
            }
            



        }
        else
        {
            //AddGraphicsI(currID);
            AddInventoryItem(currID, items[int.Parse(es.currentSelectedGameObject.name)]);//при перетаскивании обьекта типо заменяется плейс на ноль
            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currItem);

            currID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }
    public void InstnantiateGameObj()
    {
        var GameObj =(GameObject) Instantiate(data.items[currItem.id].model.gameObject.gameObject, cam.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,80),Quaternion.identity);
        GameObj.name = currItem.id.ToString();
        GameObj.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(); });
        
        //newGameObj.name = currItem.id.ToString();
        currID = -1;
        movingObject.gameObject.SetActive(false);
    }

    private void SelectItem()
    {
        if (currID == -1)
        {
            //print($"{es.currentSelectedGameObject.name} was pressed!!!!!!!!!!!!!!!!!!!!!");
            currID = int.Parse(es.currentSelectedGameObject.name);

            movingObject.GetComponent<SpriteRenderer>().sprite = data.items[currID].model.GetComponent<SpriteRenderer>().sprite;
            
            movingObject.GetComponent<Transform>().localScale = data.items[currID].model.GetComponent<Transform>().localScale;
            movingObject.gameObject.SetActive(true);

            Destroy(es.currentSelectedGameObject);



        }
        else
        {
            print($"{es.currentSelectedGameObject.name} - меня нажалиии!");
            currID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {


        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        //movingObject.transform.position = cam.ScreenToWorldPoint(pos);
        movingObject.GetComponent<Transform>().position = cam.ScreenToWorldPoint(pos);
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id !=0 && items[i].count>1)
            {
                items[i].gameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].gameObject.GetComponentInChildren<Text>().text = "";
            }
            //if (items[i].id == 0)
            //{
            //    //items.Remove(items[i]);
            //    //Destroy(InventoryMainObject.GetComponentsInChildren<GameObject>()[i]);
            //}
            items[i].gameObject.GetComponentInChildren<Image>().sprite = data.items[items[i].id].image;
        } 

    }

    public void ItemGoingBack(string values)
    {
        var strs = values.Split();
        AddGraphicsI(int.Parse(strs[3]));
        savingValues.fixedObjects.Remove(new FixedObjects(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]), int.Parse(strs[3])));
        Save();
    }
    

    
    
    
    
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();
        New.id = old.id;
        New.gameObject = old.gameObject;
        New.count = old.count;
        return New;
    }
    public void FixObj(string values)
    {
        var strs = values.Split();
        if (!savingValues.fixedObjects.Contains(new FixedObjects(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]), int.Parse(strs[3]))))
        {
        savingValues.fixedObjects.Add(new FixedObjects(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]),int.Parse(strs[3])));
        }
        print("fixed");
        Save();
    }
    public void Save()
    {
        //string json = JsonUtility.ToJson(myObject);
        //myObject = JsonUtility.FromJson<MyClass>(json);
        List<ItemIdCount> newList = new List<ItemIdCount>();
        for (int i = 0; i < items.Count; i++)
        {
            newList.Add(new ItemIdCount(items[i].id,items[i].count));
        }
        savingValues.items = newList;
        File.WriteAllText(Application.dataPath + "/save.gamesave", JsonUtility.ToJson(savingValues));
        print("saved");

    }
    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.gamesave"))
        {
            string jsonText = File.ReadAllText(Application.dataPath + "/save.gamesave");
            savingValues = JsonUtility.FromJson<SavingValues>(jsonText);
            print($"{Application.dataPath + "/save.gamesave"} exist");
        }
        else
        {
            savingValues = new SavingValues(0, 0, new List<FixedObjects>(), new List<ItemIdCount>());
            Save();
        }

        GameObject.Find("coinAmount").GetComponent<Text>().text = savingValues.coins.ToString();
        GameObject.Find("diamondAmount").GetComponent<Text>().text = savingValues.diamonds.ToString();
        foreach (var obj in savingValues.fixedObjects)
        {
            var GameObj = (GameObject)Instantiate(data.items[obj.id].model.gameObject.gameObject, new Vector3(obj.x,obj.y,obj.z), Quaternion.identity);
            GameObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObj.name = obj.id.ToString();
           // GameObj.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(); });
        }
        int indForInv=0;
        foreach (var inv in savingValues.items)
        {
            //if (items.Count == 0)
            //{
            //    AddGraphics();
            //}
            // х*ета рандомная :3

            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;
            newItem.name = indForInv.ToString();
            ItemInventory ii = new ItemInventory();
            ii.gameObject = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button itemButton = newItem.GetComponent<Button>();

            itemButton.onClick.AddListener(delegate
            {
                SelectObject();
            });
            items.Add(ii);


            //for (int i = 0; i < itemCount; i++)
            //{
            //    AddItem(i, data.items[i], Random.Range(1, 5));
            //}
            //UpdateInventory();
            
            AddItem(indForInv, data.items[inv.id],inv.count);
            indForInv++;
        }
        UpdateInventory();
    }
    
}
[System.Serializable]

public struct SavingValues
{
    public int coins;
    public int diamonds;
    public List<FixedObjects> fixedObjects;
    public List<ItemIdCount> items;

    public SavingValues(int coins, int diamonds, List<FixedObjects> fixedObjects, List<ItemIdCount> items)
    {
        this.coins = coins;
        this.diamonds = diamonds;
        this.fixedObjects = fixedObjects;
        this.items = items;
    }
    
}
[System.Serializable]

public struct ItemIdCount
{
    public int id;
    public int count;

    public ItemIdCount(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}
[System.Serializable]

public struct FixedObjects
{
    public float x,y,z;
    public int id;

    public FixedObjects(float x, float y, float z, int id)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.id = id;
    }
}
[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject gameObject;// (?)
    public int count;

}