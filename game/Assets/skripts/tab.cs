using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tab : MonoBehaviour
{
    // Start is called before the first frame update
    public EventSystem es;
    // public Dictionary<string, GameObject> tabs;
    public GameObject inv_tab;
    public GameObject cat;
    bool isOpen;
    void Start()
    {
        isOpen = inv_tab.activeSelf;
        GetComponent<Button>().onClick.AddListener(delegate
        {
            OnMouseDown();
        });
        CheckCat();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnMouseDown()
    {

        print("box");
        isOpen = !isOpen;
        inv_tab.SetActive(isOpen);
        CheckCat();

    }
    private void CheckCat()
    {
        if (isOpen)
        {
            cat.SetActive(false);
        }
        else
        {
            cat.SetActive(true);
        }
    }


}
