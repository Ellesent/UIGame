using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region Variables

    float speed = 5.0f;
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
        if (collision.gameObject.tag == "Pickup")
        {
            inventory.AddItem(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            Debug.Log("Staying in collide box");
            canhide = true;
           
        }
    }


        private void Hide()
        {
            Debug.Log("hide");
            GetComponent<SpriteRenderer>().enabled = false;
            hiding = true;
            //rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        private void UnHide()
        {
            Debug.Log("not hide");
            GetComponent<SpriteRenderer>().enabled = true;
            hiding = false;
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
