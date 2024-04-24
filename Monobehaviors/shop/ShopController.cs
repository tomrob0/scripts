using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;
using System;
using System.IO;


public class ShopController : MonoBehaviour
{

    public TextMeshPro playerTMP, item0TMP,item1TMP,item2TMP,item3TMP;
    // Start is called before the first frame update
    void Start()
    {
        //show current pellets, and Item cost.
        this.updatePlayerTMP();
        this.item0TMP.text =""+ ItemsSingleton.chickenItemCost;
        this.item1TMP.text =""+ ItemsSingleton.cherryItemCost;
        this.item2TMP.text =""+ ItemsSingleton.appleItemCost;
        this.item3TMP.text =""+ ItemsSingleton.magiccherryItemCost;

        //this.readItemsData();
        string jsonString = this.readItemsDataJson();

        
        RootObject root = JsonUtility.FromJson<RootObject>(jsonString);
        foreach (var item in root.items)
        {
            print($"Name: {item.name}, Stat Impacted: {item.stat_impacted}, Modifier: {item.modifier}");
            
        }

    
    
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
        if(MySingleton.currentPellets >= ItemsSingleton.chickenItemCost)
            MySingleton.currentPellets -= ItemsSingleton.chickenItemCost;
            MySingleton.thePlayer.addHP(5);
            this.updatePlayerTMP();

        print("Item 1 selected:");


     }
        if(Input.GetKeyUp(KeyCode.Alpha2))
     {
        //charge pellets
        if(MySingleton.currentPellets >= ItemsSingleton.cherryItemCost)
            MySingleton.currentPellets -= ItemsSingleton.cherryItemCost;
            MySingleton.thePlayer.addHP(5);
            this.updatePlayerTMP();

        print("Item 2 selected: ");


     }
     if(Input.GetKeyUp(KeyCode.Alpha3))
     {
        //charge pellets
        if(MySingleton.currentPellets >= ItemsSingleton.appleItemCost)
            MySingleton.currentPellets -= ItemsSingleton.appleItemCost;
            MySingleton.thePlayer.addHP(5);
            this.updatePlayerTMP();

        print("Item 3 selected:");


     }
    if(Input.GetKeyUp(KeyCode.Alpha4))
     {
        //charge pellets
        if(MySingleton.currentPellets >= ItemsSingleton.magiccherryItemCost)
            MySingleton.currentPellets -= ItemsSingleton.magiccherryItemCost;
            MySingleton.thePlayer.addHP(5);
            this.updatePlayerTMP();

        print("Item 4 selected:" );


     }
     if(Input.GetKeyUp(KeyCode.Alpha1) && MySingleton.currentPellets < ItemsSingleton.chickenItemCost)
     {
        //charge pellets
        
            this.updatePlayerTMP();

        print("**Not enough pellets**");


     }
     if(Input.GetKeyUp(KeyCode.Alpha2) && MySingleton.currentPellets < ItemsSingleton.cherryItemCost)
     {
        //charge pellets
        if(MySingleton.currentPellets < ItemsSingleton.cherryItemCost)
            this.updatePlayerTMP();

        print("**Not enough pellets**");


     }
    if(Input.GetKeyUp(KeyCode.Alpha3) && MySingleton.currentPellets < ItemsSingleton.appleItemCost)
     {
        //charge pellets
        if(MySingleton.currentPellets < ItemsSingleton.appleItemCost)
            this.updatePlayerTMP();

        print("**Not enough pellets**");


     }
    if(Input.GetKeyUp(KeyCode.Alpha4) && MySingleton.currentPellets < ItemsSingleton.magiccherryItemCost)
     {
        //charge pellets
        if(MySingleton.currentPellets < ItemsSingleton.magiccherryItemCost)
            this.updatePlayerTMP();

        print("**Not enough pellets**");


     }
     








    }
    private void readItemsData()
    {
        string filePath = "Assets/data files/items_data.txt";
        string answer = "";

        if (File.Exists(filePath))
        {
            try
            {
               
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    string[] itemParts = new string[3];

                    int pos = 0;
                    
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(",");
                        for (int i = 0; i < parts.Length; i++)
                        {
                            print(parts[i]);
                            itemParts[pos % 3] = parts[i];
                            pos++;
                        }
                        print("item info");
                        Item theItem = new Item(itemParts[0], itemParts[1], int.Parse(itemParts[2]));
                        theItem.display();
                    }
                }
            }
            catch (Exception ex)
            {
                
                print("file could not be read");
                print(ex.Message);
            }
        }
        else
        {
            print("The file does not exist.");
        }
    }

    private string readItemsDataJson()
    {
        string filePath = "Assets/data files/items_data_json.txt"; // Path to the file
        string answer = "";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            try
            {
                print("Serialized JSON Parsing");
                // Open the file to read from
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    // Read and display lines from the file until the end of the file is reached
                    while ((line = reader.ReadLine()) != null)
                    {
                        answer = answer + line;
                    }
                    return answer;
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occurred during reading the file
                print("An error occurred while reading the file:");
                print(ex.Message);
                return null;
            }
        }
        else
        {
            print("The file does not exist.");
            return null;
        }
    }


}

