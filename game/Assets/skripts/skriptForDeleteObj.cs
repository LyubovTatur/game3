using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skriptForDeleteObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "HouseScene")
        {
            try
            {
                if (GameObject.Find("inventory_tab").activeSelf)
                {

                    Destroy(gameObject);
                    GameObject.Find("inventory_tab").SendMessageUpwards("ItemGoingBack", $"{transform.position.x} {transform.position.y} {transform.position.z} {int.Parse(name)}");
                }
            }
            catch (System.Exception)
            {

            }
        }
        if (SceneManager.GetActiveScene().name == "FurnitureShopScene")
        {
            GameObject.Find("Main Camera").SendMessage("ClickOnItemInShop", int.Parse(name));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name == "HouseScene")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObject.Find("inventory_tab").SendMessageUpwards("FixObj", $"{transform.position.x} {transform.position.y} {transform.position.z} {int.Parse(name)}");
            print("sended");
        }
    }
    [System.Serializable]
    public struct FixedObjects
    {
        public float x, y, z;
        public int id;

        public FixedObjects(float x, float y, float z, int id)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.id = id;
        }
    }
}
