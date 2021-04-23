using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkriptPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelosity;
    //public GameObject gameObject;
    private bool isFliped = false, flipState = false; 
    private SpriteRenderer sr;
    Animator anim;
    public Camera camera;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (camera.transform.position.x+1>0)
            {

                

                Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Flip(mouse);
                // transform.localScale.Set(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

                //transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Time.deltaTime * speed);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(mouse.x, transform.position.y), speed * Time.deltaTime);
                //anim.Play("walk");
                anim.SetBool("IsWalking", true);
                //transform.position = Vector2.MoveTowards(transform.position,mouse, speed * Time.deltaTime);
                camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y,-10);
                    //position.x = transform.position.x;
            }
            else
            {
                transform.position = new Vector2(0, transform.position.y);
                camera.transform.position = new Vector3(0, camera.transform.position.y, camera.transform.position.z);
                //anim.SetBool("IsWalking", false);

            }

        }
        else
        {
            anim.SetBool("IsWalking", false);
           // anim.Play("idle");
        }
    }

    private void Flip(Vector2 mouse)
    {
        if (mouse.x < transform.position.x)
        {
            isFliped = true;
        }
        else
        {
            isFliped = false;
        }
        if (isFliped)
        {
            if (!flipState)
            {
                transform.localScale *= new Vector2(-1, 1);
                flipState = true;

            }
        }
        else
        {
            if (flipState)
            {
                transform.localScale *= new Vector2(-1, 1);

                flipState = false;

            }
        }
    }

    void FixedUpdate()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


    }
}
