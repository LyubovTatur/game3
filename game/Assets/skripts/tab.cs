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
    bool isOpen;
    void Start()
    {
        isOpen = inv_tab.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            OpenClose(); 
        });
        
    }
    public void OpenClose()
    {

        print("box");
        isOpen = !isOpen;
        inv_tab.SetActive(isOpen);
    }
}
