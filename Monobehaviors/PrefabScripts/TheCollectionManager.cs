using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheCollectionManager : MonoBehaviour
{


    public GameObject itemPrefab;
    public Material placeHolderMaterial, secondMaterial;
    private Vector3 startPosition;
    private int itemSpawned = 0;
    private int currentLeftPos = 0;
    private List<GameObject> theItemsGO = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            this.startPosition = new Vector3(-3.7f + (this.itemSpawned * 2.51f), 0f, 0f);
            GameObject newObect = Instantiate(this.itemPrefab, this.startPosition, Quaternion.identity);
            TextMeshPro tmp = newObect.transform.GetChild(0).GetComponent<TextMeshPro>();
            tmp.SetText("Item " + i + "\nCost: 5");
            newObect.transform.SetParent(this.gameObject.transform);
            newObect.transform.localPosition = this.startPosition;
            newObect.transform.localRotation = Quaternion.identity;
            if (this.itemSpawned >= 4)
            {
                newObect.SetActive(false);
            }
            this.itemSpawned++;
            this.theItemsGO.Add(newObect);
        }



        string jsonString = MySingleton.readJsonString();
        RootObject root = JsonUtility.FromJson<RootObject>(jsonString);





    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) && this.currentLeftPos <= this.theItemsGO.Count - 5)
        {
            //move everything left one item
            GameObject tempLeft = null, tempRight = null;

            //goes through our items and moves them appropriately and makes them visible/invisible as needed
            for (int i = 0; i < this.theItemsGO.Count; i++)
            {
                this.theItemsGO[i].transform.Translate(Vector3.left * 10.0f);
                if (i == this.currentLeftPos)
                {
                    tempLeft = this.theItemsGO[i];
                }

                if (i == this.currentLeftPos + 4)
                {
                    tempRight = this.theItemsGO[i];
                }
            }
            tempLeft.SetActive(false);
            tempRight.SetActive(true);
            this.currentLeftPos++;

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && this.currentLeftPos >= 1)
        {
            //move everything left one item
            GameObject tempLeft = null, tempRight = null;

            for (int i = 0; i < this.theItemsGO.Count; i++)
            {
                this.theItemsGO[i].transform.Translate(Vector3.right * 10.0f);
                if (i == this.currentLeftPos - 1)
                {
                    tempLeft = this.theItemsGO[i];
                }

                if (i == this.currentLeftPos + 3)
                {
                    tempRight = this.theItemsGO[i];
                }
            }
            tempLeft.SetActive(true);
            tempRight.SetActive(false);
            this.currentLeftPos--;
        }
    }

}