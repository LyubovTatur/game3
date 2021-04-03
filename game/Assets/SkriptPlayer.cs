using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkriptPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelosity;
    //public GameObject gameObject;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {


            //transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Time.deltaTime * speed);
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(mouse.x,transform.position.y), speed * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position,mouse, speed * Time.deltaTime);

            //position.x = transform.position.x;
        }
    }
    void FixedUpdate()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


    }
}
