using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;




public class pokemonAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("https://pokeapi.co/api/v2/pokemon/?offset=0&limit=2000"));        
    }


    IEnumerator GetRequest(string uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                print("Error: " + webRequest.error);
            }
            else
            {
                string jsonString = webRequest.downloadHandler.text;
                CollectionOfPokemon theCollectionOfPokemon = JsonUtility.FromJson<CollectionOfPokemon>(jsonString);
                theCollectionOfPokemon.display();
                
            }
        }

    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
