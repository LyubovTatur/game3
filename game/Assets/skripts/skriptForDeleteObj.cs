using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        try
        {
            if (GameObject.Find("inventory_tab").activeSelf)
            {
                
                Destroy(gameObject);
                GameObject.Find("inventory_tab").SendMessageUpwards("ItemGoingBack", int.Parse(name));
            }
        }
        catch (System.Exception)
        {

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        

    }
}
