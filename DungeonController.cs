using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{

    public GameObject[] closedDoors;
   
   // Start is called before the first frame update
   private string mapIndexToStringForExit(int index)
   {
        if(index == 0)
        {return "north";
        }
        else if(index == 1)
        {return "south";
        }
        else if(index == 2)
        {return "east";
        }
        else if(index == 3)
        {return "west";
        }
        else
        {
            return "?";
        }


   }
    void Start()
    {
        MySingleton.theCurrentRoom = new Room("a room");
        MySingleton.addRoom(MySingleton.theCurrentRoom);


        int openDoorIndex = Random.Range(0,4);
       this.closedDoors[openDoorIndex].SetActive(false);
        MySingleton.theCurrentRoom.setOpenDoor(this.mapIndexToStringForExit(openDoorIndex));

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
