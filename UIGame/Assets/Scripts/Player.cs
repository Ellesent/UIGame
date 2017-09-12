using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region Variables

    float speed = 7.0f;
    public Inventory inventory;
    bool hiding;
    Rigidbody2D rb;
    bool canhide = false;
    #endregion

    #region Properties

    public bool IsHiding()
    {
        return hiding;
    }

    #endregion


    // Use this for initialization
    void Start()
    {
        if (inventory == null)
        {
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        }
        hiding = false;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canhide)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (hiding)
                {
                    UnHide();
                }
                else
                {
                    Hide();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            canhide = true;
           
        }
        if (collision.gameObject.tag == "Pickup" && Input.GetKeyDown(KeyCode.Space))
        {
            inventory.AddItem(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            canhide = false;

        }
    }


    private void Hide()
    { 
            GetComponent<SpriteRenderer>().enabled = false;
            hiding = true;
            GetComponent<CircleCollider2D>().enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

        private void UnHide()
        {
            GetComponent<SpriteRenderer>().enabled = true;
            hiding = false;
            GetComponent<CircleCollider2D>().enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
