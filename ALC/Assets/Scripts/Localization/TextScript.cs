using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public string keyWord;
    
    private void LateUpdate()
    {
        
  
       GetComponent<Text>().text = FindObjectOfType<Localizator>().words[keyWord];
    }
}
