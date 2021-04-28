using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCatsCollisions : MonoBehaviour
{
    public Sprite bonk;
    public Sprite cat;
    
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
            print("ouch");
        GetComponent<SpriteRenderer>().sprite = bonk;
        
    }
    private void OnMouseUp()
    {
        GameObject.Find("Main Camera").SendMessage("WasBonked");
        GetComponent<SpriteRenderer>().sprite = cat;

    }

}
