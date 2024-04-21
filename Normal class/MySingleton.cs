using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MySingleton
{

    public static int currentPellets = 0;
    public static string currentDirection = "?";
    public static Player thePlayer;
    public static Dungeon theDungeon = MySingleton.generateDungeon();
   

    public static string flipDirection(string direction)
    {
        if(direction.Equals("north"))
        {
            return "south";
        }
        else if(direction.Equals("south")) 
        {
            return "north";
        }
        else if(direction.Equals("east")) 
        {
            return "west";
        }
        else if(direction.Equals("west")) 
        {
            return "east";
        }
        else 
        {
            Debug.Log("not legal direction inside of MySingleton");
            return "na";
        }
    }
    private static Dungeon generateDungeon()
    
    
    {
        Room r1 = new Room("R1");
        Room r2 = new Room("R2");
        Room r3 = new Room("R3");
        Room r4 = new Room("R4");
        Room r5 = new Room("R5");
        Room r6 = new Room("R6");
        r1.addExit("north", r2);
        r2.addExit("south", r1);
        r2.addExit("north", r3);
        r3.addExit("south", r2);
        r3.addExit("west", r4);
        r3.addExit("north", r6);
        r3.addExit("east", r5);
        r4.addExit("east", r3);
        r5.addExit("west", r3);
        r6.addExit("south", r3);

        Dungeon theDungeon = new Dungeon("the cross");
        
        theDungeon.setStartRoom(r1);
        MySingleton.thePlayer = new Player("TOM");
        theDungeon.addPlayer(MySingleton.thePlayer);
        return theDungeon;
    }
   
}
   




