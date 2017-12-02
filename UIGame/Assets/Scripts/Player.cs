using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    #region Variables

    float speed = 7.0f;
    public Inventory inventory;
    bool hiding;
    Rigidbody2D rb;
    bool canhide = false;
    AudioSource audioSource;
    AudioClip outsideFootsteps;
    AudioClip insideFootsteps;

    AudioClip currentFootsteps;

    float timeFootsteps;

    Scene currentScene;


    Image terrorBar;

    // Used for audio
    bool isMoving;

    static bool hasBeenHere = false;

    // Event Handling
    EnterHouseEvent enterHouseEvent;
    EnterHouseSetNextQuestEvent setNextQuest;
    KeyEvent keyEvent;
    UseKeyEvent useKeyEvent;
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
        useKeyEvent = new UseKeyEvent();
        enterHouseEvent = new EnterHouseEvent();
        setNextQuest = new EnterHouseSetNextQuestEvent();
        keyEvent = new KeyEvent();
        EventManager.EnterHouseInvoker(this);
        EventManager.PickupKeyInvoker(this);
        EventManager.DoorInvoker(this);

        // Mark Entering the house as complete
        if (SceneData.currentScene == "InteriorHouse" && SceneData.previousScene == "GameScene1")
        {
            enterHouseEvent.Invoke("Enter the house", "You have just arrived at the address, and there is only a house. Enter through the front door.");
            //setNextQuest.Invoke("Explore the house");
        }

        if (SceneData.currentScene == "Room2" && !hasBeenHere)
        {
            Debug.Log("Event being invoked?");
            useKeyEvent.Invoke("Find out where the key goes", "You have found a key. This probably opens a door somewhere");

        }

        // Get the audio component and clip attached to audio source
        audioSource = GetComponent<AudioSource>();
        outsideFootsteps = audioSource.clip;
       
        // Set timer for footsteps
        timeFootsteps = 0f;

        // Get audio file for inside from Resources Folder
        insideFootsteps = Resources.Load("Sounds/indoorfootstep") as AudioClip;

        // Get the current scene, and set if footsteps are indoors or outdoors appropriately
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GameScene1")
        {
            currentFootsteps = outsideFootsteps;
        }
        else
        {
            currentFootsteps = insideFootsteps;
        }

        // Set the audio source's clip to the correct footstep clip
        audioSource.clip = currentFootsteps;

        isMoving = false;
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
            if (Input.GetKeyDown(InputFields.interact))
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

        if (Input.GetKeyDown(InputFields.left) || Input.GetKeyDown(InputFields.right) || Input.GetAxis(InputFields.joystickAxis) > 0 || Input.GetAxis(InputFields.joystickAxis) < 0)
        {
            isMoving = true;
        }

        if (Input.GetKeyUp(InputFields.left) || Input.GetKeyUp(InputFields.right) || (Input.GetJoystickNames().Length > 0 && Input.GetAxis(InputFields.joystickAxis) == 0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            PlaySound();
        }
    }
       
    /// <summary>
    /// Movement code
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "")
        {
            rb.MovePosition( transform.position + new Vector3(Input.GetAxis(InputFields.joystickAxis), 0) * speed * Time.deltaTime);
        }
            if (Input.GetKey(InputFields.left))
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKey(InputFields.right))
        {
            rb.MovePosition(transform.position + new Vector3(1, 0) * speed * Time.deltaTime);

        }
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

        if (collision.gameObject.tag == "Pickup" && Input.GetKeyDown(InputFields.interact))
        {
            inventory.AddItem(collision.gameObject.name);

            if (collision.gameObject.name == "tempkey")
            {
                keyEvent.Invoke("Find out where the key goes");
            }
            collision.gameObject.GetComponent<Pickupable>().Destroy();
           
        }

        if (collision.gameObject.tag == "Edible" && Input.GetKeyDown(InputFields.interact))
        {
            inventory.AddNumEdibles();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Water" && inventory.GetEquippedItem() == "SprayBottle")
        {
            Debug.Log("Water and Spray Bottle");
            inventory.ChangeItemName("SprayBottle", "SprayBottleWater");
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

    void PlaySound()
    {
        if (timeFootsteps <= 0)
        {
            audioSource.Play();
            timeFootsteps = 0.5f;
        }

        timeFootsteps -= Time.deltaTime;
    }

    public void AddEnterHouseListener(UnityAction<string,string> method)
    {
        enterHouseEvent.AddListener(method);
    }

    public void AddKeyListener(UnityAction<string> method)
    {
        keyEvent.AddListener(method);
    }

    public void AddDoorListener(UnityAction<string, string> method)
    {
        Debug.Log("Being listened to?");
        useKeyEvent.AddListener(method);
    }

}
