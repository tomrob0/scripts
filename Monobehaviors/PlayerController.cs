
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject northExit;
    public GameObject southExit;
    public GameObject eastExit;
    public GameObject westExit;
    public GameObject middleOfTheRoom;
    private float speed = 5.0f;
    private bool amMoving = false;
    private bool amAtMiddleOfRoom = false;

    private void turnOffExits()
    {
        this.northExit.gameObject.SetActive(false);
        this.southExit.gameObject.SetActive(false);
        this.eastExit.gameObject.SetActive(false);
        this.westExit.gameObject.SetActive(false);

    }

    private void turnOnExits()
    {
        this.northExit.gameObject.SetActive(true);
        this.southExit.gameObject.SetActive(true);
        this.eastExit.gameObject.SetActive(true);
        this.westExit.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

        //disable all exits when the scene first loads
        this.turnOffExits();

        //disable the middle collider until we know what our initial state will be
        //it should already be disabled by default, but for clarity, lets do it here
        this.middleOfTheRoom.SetActive(false);

        if (!MySingleton.currentDirection.Equals("?"))
        {
            //mark ourselves as moving since we are entering the scene through one of the exits
            this.amMoving = true;

            //we will be positioning the player by one of the exits so we can turn on the middle collider
            this.middleOfTheRoom.SetActive(true);
            this.amAtMiddleOfRoom = false;

            if (MySingleton.currentDirection.Equals("north"))
            {
                this.gameObject.transform.position = this.southExit.transform.position;
                this.gameObject.transform.LookAt(this.northExit.transform.position);
                //rb.MovePosition(this.southExit.transform.position);
            }
            else if (MySingleton.currentDirection.Equals("south"))
            {
                this.gameObject.transform.position = this.northExit.transform.position;
                this.gameObject.transform.LookAt(this.southExit.transform.position);
                //rb.MovePosition(this.northExit.transform.position);
            }
            else if (MySingleton.currentDirection.Equals("west"))
            {
                this.gameObject.transform.position = this.eastExit.transform.position;
                this.gameObject.transform.LookAt(this.westExit.transform.position);
                //rb.MovePosition(this.eastExit.transform.position);
            }
            else if (MySingleton.currentDirection.Equals("east"))
            {
                this.gameObject.transform.position = this.westExit.transform.position;
                this.gameObject.transform.LookAt(this.eastExit.transform.position);
                //rb.MovePosition(this.westExit.transform.position);
            }
            //StartCoroutine(turnOnMiddle());
        }
        else
        {
            //We will be positioning the play at the middle
            //so keep the middle collider off for this run of the scene
            this.amMoving = false;
            this.amAtMiddleOfRoom = true;
            this.middleOfTheRoom.SetActive(false);
            this.gameObject.transform.position = this.middleOfTheRoom.transform.position;
        }
    }

    /*
    IEnumerator turnOnMiddle()
    {
        yield return new WaitForSeconds(1);
        this.middleOfTheRoom.SetActive(true);
        print("turned on");

    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("door"))
        {
            print("Loading scene");

            //remove the player from the current room and place him into the destination, prior to loading the new scene
            MySingleton.thePlayer.getCurrentRoom().removePlayer(MySingleton.currentDirection);

            EditorSceneManager.LoadScene("Scene 1");
        }
        else if(other.CompareTag("power-pellet"))
        {
            other.gameObject.SetActive(false); //make pellet disappear
            Room theCurrentRoom = MySingleton.thePlayer.getCurrentRoom();
            theCurrentRoom.removePellet(MySingleton.currentDirection);
        }
        
        else if(other.CompareTag("middleOfTheRoom") && !MySingleton.currentDirection.Equals("?"))
        {
            //we have hit the middle of the room, so lets turn off the collider
            //until the next run of the scene to avoid additional collisions
            this.middleOfTheRoom.SetActive(false);
            this.turnOnExits();

            print("middle");
            this.amAtMiddleOfRoom = true;
            this.amMoving = false;
            MySingleton.currentDirection = "middle";
        }
        else
        {
            print("**");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("north"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "north";
            this.gameObject.transform.LookAt(this.northExit.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("south"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "south";
            this.gameObject.transform.LookAt(this.southExit.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("west"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "west";
            this.gameObject.transform.LookAt(this.westExit.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("east"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "east";
            this.gameObject.transform.LookAt(this.eastExit.transform.position);

        }

        //make the player move in the current direction
        if (MySingleton.currentDirection.Equals("north"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.northExit.transform.position, this.speed * Time.deltaTime);
        }

        if (MySingleton.currentDirection.Equals("south"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.southExit.transform.position, this.speed * Time.deltaTime);
        }

        if (MySingleton.currentDirection.Equals("west"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.westExit.transform.position, this.speed * Time.deltaTime);
        }

        if (MySingleton.currentDirection.Equals("east"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.eastExit.transform.position, this.speed * Time.deltaTime);
        }
    }
}
