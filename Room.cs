using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
   private string name;
   private string openDoorDirection;

   public Room(string name)
   {
     this.name = name;
   }

   public void setOpenDoor(string direction)
   {
    this.openDoorDirection = direction;
   }
   public bool isOpenDoor(string direction)
   {
      return openDoorDirection.Equals(direction);
   }
   










}
