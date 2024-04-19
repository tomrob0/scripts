using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{

    public GameObject[] closedDoors;
   
   
   
    void Start()
    {
       Room theCurrentRoom = MySingleton.thePlayer.getCurrentRoom();
       
       if(theCurrentRoom.hasExit("north"))
       {
        this.closedDoors[0].SetActive(true);
       }
        if(theCurrentRoom.hasExit("south"))
       {
        this.closedDoors[1].SetActive(true);
       }
        if(theCurrentRoom.hasExit("east"))
       {
        this.closedDoors[2].SetActive(true);
       }
        if(theCurrentRoom.hasExit("west"))
       {
        this.closedDoors[3].SetActive(true);
       }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
