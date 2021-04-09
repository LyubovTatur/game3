using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public RectTransform movingObject;
    public Vector3 offset;

    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }
        // х*ета рандомная :3
        for (int i = 0; i < itemCount; i++)
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 99));
        }
        UpdateInventory();

    }

    public void Update()
    {
        if (currID!=-1)
        {
            MoveObject();
        }
        if (Input.GetMouseButtonDown(1))
        {
            InstnantiateGameObj();
        }
    }
    public void InsertIntoInv(int id, Item item, int count)
    {

    }

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
    public void RemoveFromInv(int InvIndx)
    {

        items.Remove(items[InvIndx]);
        // List<ItemInventory> tempItems = new List<ItemInventory>();
       //for (int i = 0; i < InventoryMainObject.transform.childCount; i++)
       //{
       //   // Destroy(InventoryMainObject.transform.GetChild(i));
       //}

        //for (int i = 0; i < items.Count; i++)
        //{
          //  AddGraphicsI(i);
            // UpdateInventory();
       // }
    }   //

    public void SelectObject()
    {
        if (currID == -1)
        {

            currID = int.Parse(es.currentSelectedGameObject.name);
            currItem = CopyInventoryItem(items[currID]);
            //currItem = CopyInventoryItem(items[currID]);
            movingObject.GetComponent<Image>().sprite = data.items[currItem.id].model.GetComponent<SpriteRenderer>().sprite;
            Vector2 temp = cam.WorldToScreenPoint(data.items[currItem.id].model.GetComponent<SpriteRenderer>().size);
            movingObject.GetComponent<RectTransform>().sizeDelta = temp;
            movingObject.GetComponent<RectTransform>().localScale = data.items[currItem.id].model.GetComponent<Transform>().localScale;
            //AddItem(currID, data.items[0], 0);//при перетаскивании обьекта типо заменяется плейс на ноль
            movingObject.gameObject.SetActive(true);
            //items.Remove(items[currID]);
            RemoveFromInv(currID);


        }
        else
        {
            AddGraphicsI(currID);
            AddInventoryItem(currID, items[int.Parse(es.currentSelectedGameObject.name)]);//при перетаскивании обьекта типо заменяется плейс на ноль
            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currItem);

            currID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }
    public void InstnantiateGameObj()
    {
        Instantiate(data.items[currItem.id].model, cam.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,1),Quaternion.identity);
        currID = -1;
        movingObject.gameObject.SetActive(false);
    }
    public void MoveObject()
    {


        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (items[i].id !=0 && items[i].count>1)
            {
                items[i].gameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].gameObject.GetComponentInChildren<Text>().text = "";
            }

            items[i].gameObject.GetComponentInChildren<Image>().sprite = data.items[items[i].id].image;
        } 

    }

    
    

    
    
    
    
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();
        New.id = old.id;
        New.gameObject = old.gameObject;
        New.count = old.count;
        return New;
    }
}
[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject gameObject;// (?)
    public int count;

}