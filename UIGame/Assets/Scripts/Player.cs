using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    #region Variables

    float speed = 7.0f;
    public Inventory inventory;
    bool hiding;
    Rigidbody2D rb;
    bool canhide = false;

    Image terrorBar;
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
        StartPosition();
       
        try
        {
            terrorBar = GameObject.Find("TerrorBar").GetComponent<Image>();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Couldn't find terrorBar, moving on");
        }

        if (inventory == null)
        {
            try
            {
                inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            }
            catch (NullReferenceException)
            {
                Debug.Log("Can't find inventory, moving on");
            }

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

        if (Input.GetKeyDown(KeyCode.E) && inventory.GetNumEdibles() > 0)
        {
            terrorBar.fillAmount += 0.3f;
            inventory.RemovedNumEdibles();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


    }

    // TODO don't check for input in OnTriggerStay
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            canhide = true;

        }

        if (collision.gameObject.tag == "Pickup" && Input.GetKeyDown(KeyCode.Space))
        {
            inventory.AddItem(collision.gameObject.name);
            collision.gameObject.GetComponent<Pickupable>().Destroy();
           
        }

        if (collision.gameObject.tag == "Edible" && Input.GetKeyDown(KeyCode.Space))
        {
            inventory.AddNumEdibles();
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

    void StartPosition()
    {
        if (SceneData.currentScene == "InteriorHouse")
        {
            if (SceneData.previousScene == "Room1")
            {
                transform.position = GameObject.Find("Spawn1").transform.position;
            }

            else if (SceneData.previousScene == "Room2")
            {
                transform.position = GameObject.Find("Spawn2").transform.position;
            }
        }

    }

}
