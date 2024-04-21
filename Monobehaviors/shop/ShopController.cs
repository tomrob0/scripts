using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;


public class ShopController : MonoBehaviour
{

    public TextMeshPro playerTMP, itemTMP;
    // Start is called before the first frame update
    void Start()
    {
        //show current pellets, and Item cost.
        this.updatePlayerTMP();
        this.itemTMP.text =""+ ItemsSingleton.cherryItemCost;
    
    
    }   
    // Update is called once per frame

    private void updatePlayerTMP()
    {
        this.playerTMP.text = ""+ MySingleton.currentPellets;
    }
   
    void Update()
    {
     if(Input.GetKeyUp(KeyCode.Alpha1))
     {
        //charge pellets
        if(MySingleton.currentPellets >= ItemsSingleton.cherryItemCost)
            MySingleton.currentPellets -= ItemsSingleton.cherryItemCost;
            MySingleton.thePlayer.addHP(5);
            this.updatePlayerTMP();

        print("Item 1 selected: + *5HP*");


     }
    
    }
}
