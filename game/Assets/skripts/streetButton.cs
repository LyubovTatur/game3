using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class streetButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            OnMouseDown();
        });
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    private void OnMouseDown()
    {
        print("shop");
        //Application.LoadLevel("StreetScene");
        SceneManager.LoadScene("StreetScene");
    }
}
