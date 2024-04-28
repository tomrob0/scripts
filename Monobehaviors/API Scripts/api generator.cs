using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class apigenerator : MonoBehaviour
{
   private string readAPIdata()
    {
        string filePath ="C:\Users\trobertson\OneDrive\Desktop\unity\My project\Assets\scripts\Monobehaviors\API Scripts\api_input.json";
        string answer ="";

        if(File.Exists(filePath))
        {
            try
            {
                using (SystemReader reader = new StreamReader(filePath))
                {
                    string line;
                    while((line = reader.ReadLine())!= null)
                    {
                        answer = answer + line;
                    }
                    return answer;
                }
            }
        catch (Exception ex)
        {
            print("**an error happened while reading**")
            print(ex.Message);
            return null;
        }
        
        
        }



    }
    
    
    
    
    
    void Start()
    {
        string fileText = this.readAPIdata();
        string[] parts = fileText.Split(",");
        string answer = "using UnityEngine;\n\n[System.Serializable]\npublic class [ClassName]\n{\n";
        string constructorParams= "";
        string constructorBody= "";
        foreach (string s in parts)
        {
            string[] pair = s.Split(":");
            string varName = pair[0].Trim().Substring(1,pair[0].Length-2);

            if(pair[1].Trim()[0] =='"')
            {
                answer+= "\tpublic string "+varName +";\n";
                constructorParams+= "string "+varName +",";
            }
            else if(pair[1][0] =='{')
            {
                answer+= "\tpublic [Another Object Type] "+varName +";\n";
                constructorParams+= "[Another Object Type] "+varName +",";
            }
            else
            {
                answer+= "\tpublic int "+varName +";\n";
                constructorParams+= "int "+varName +",";

            }
        
            constructorBody +="\n\t\tthis."+ varName +"="+varName + ";";
        }
    answer += "\n\tpublic [ClassName]("+ constructorParams.Substring(0,constructorParams.Length)
    answer += "\t{"+constructorBody+"\n\t}\n}";
    print(answer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
