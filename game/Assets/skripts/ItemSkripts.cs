using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSkripts : MonoBehaviour
{
    public int currID = -1;
    public EventSystem es;
    public GameObject movingObject;
    public database data;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { SelectObject(); });

    }

    // Update is called once per frame
    void Update()
    {
        if (currID!=-1)
        {
            MoveObject();
        }   
    }

    public void SelectObject()
    {
        if (currID == -1)
        {

            currID = int.Parse(es.currentSelectedGameObject.name);
            
            //currItem = CopyInventoryItem(items[currID]);
            //movingObject.GetComponent<Image>().sprite = data.items[currItem.id].model.GetComponent<SpriteRenderer>().sprite;
            movingObject.GetComponent<SpriteRenderer>().sprite = data.items[currID].model.GetComponent<SpriteRenderer>().sprite;
            //Vector2 temp = cam.WorldToScreenPoint(data.items[currItem.id].model.GetComponent<SpriteRenderer>().size);
            //movingObject.GetComponent<RectTransform>().sizeDelta = temp;
            movingObject.GetComponent<Transform>().localScale = data.items[currID].model.GetComponent<Transform>().localScale;
            //AddItem(currID, data.items[0], 0);//при перетаскивании обьекта типо заменяется плейс на ноль
            movingObject.gameObject.SetActive(true);
            //if (es.currentSelectedGameObject.GetComponentInChildren<Text>().text == "")
            //{
            //    print(items.Remove(items[currID]));
            //    for (int i = currID; i < items.Count; i++)
            //    {
            //        items[i].gameObject.name = (int.Parse(items[i].gameObject.name) - 1).ToString();
            //    }
            //    //RemoveFromInv(currID);
                Destroy(es.currentSelectedGameObject);
                print(es.currentSelectedGameObject.name);
            //}
            //else
            //{
            //    if (es.currentSelectedGameObject.GetComponentInChildren<Text>().text == "2")
            //    {
            //        es.currentSelectedGameObject.GetComponentInChildren<Text>().text = "";
            //    }
            //    else
            //    {
            //        es.currentSelectedGameObject.GetComponentInChildren<Text>().text = (int.Parse(es.currentSelectedGameObject.GetComponentInChildren<Text>().text) - 1).ToString();
            //    }
            //}




        }
        else
        {
            //AddGraphicsI(currID);
            //AddInventoryItem(currID, items[int.Parse(es.currentSelectedGameObject.name)]);//при перетаскивании обьекта типо заменяется плейс на ноль
            //AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currItem);

            currID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }
    public void MoveObject()
    {


        Vector3 pos = Input.mousePosition ;
        pos.z = 1;//InventoryMainObject.GetComponent<RectTransform>().position.z;
        //movingObject.transform.position = cam.ScreenToWorldPoint(pos);
        movingObject.GetComponent<Transform>().position = cam.ScreenToWorldPoint(pos);
    }

}
