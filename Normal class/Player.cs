using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Inhabitant

{
    
    

    public Player(string name) :base(name)
    {
        this.currentRoom = null;

    }
    public void resetStats()
    {
        this.hp=this.maxHP;
    }
}
