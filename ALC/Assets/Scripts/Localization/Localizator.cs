using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class KeyWord {
    public string Key { get; set; }
    public string Word { get; set; }
}

public class Localizator : MonoBehaviour
{
    public Dictionary<string, string> words = new Dictionary<string, string>();

    JArray keyWord;
    string path;
    Language activeLanguage;


    // Start is called before the first frame update

    void Start()
    {
        //GetAndParseJson();
            activeLanguage = FindObjectOfType<MenuController>().language;
            path = FindObjectOfType<MenuController>().languagePath;
            ChangeCurrentLanguage(path);
    }
    private void Update()
    {
        if (FindObjectOfType<MenuController>().reloadLanguage) {
            Debug.Log("reload");
            
            path = FindObjectOfType<MenuController>().languagePath;
            ChangeCurrentLanguage(path);
            Debug.Log(path);
            FindObjectOfType<MenuController>().reloadLanguage = false;
            foreach (var item in words)
            {
                Debug.Log($"item key : {item.Key} item value : {item.Value}");
            }
        }
        
    }
    // Update is called once per frame

    void ChangeCurrentLanguage(string path) {
        var textAsset = Resources.Load<TextAsset>(path);

        if (textAsset!=null)
        {
            words = new Dictionary<string, string>();
            Debug.Log(textAsset);
            JObject tt = JObject.Parse(textAsset.text);
            words = tt.ToObject<Dictionary<string, string>>();

        }
    }

    private void GetAndParseJson()
    {
        var textAsset = Resources.Load<TextAsset>("Data/English");
        JObject tt = JObject.Parse(textAsset.text);
        words = tt.ToObject<Dictionary<string, string>>();

        foreach(var item in words)
        {
            Debug.Log($"item key : {item.Key} item value : {item.Value}");
        }

    }


    /*void GetLocalization(JSONObject obj) {

        

        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    words.Add(key, getsome(j));
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list)
                {
                    GetLocalization(j);
                }
                break;
            case JSONObject.Type.STRING:
                 
                break;
            case JSONObject.Type.NUMBER:
                //Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                //Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                //Debug.Log("NULL");
                break;

        }
    }

    string getsome(JSONObject obj){
        switch (obj.type)
        {
        
            case JSONObject.Type.STRING:
                return obj.str;

            default:return "1";

        }
    }*/
}
