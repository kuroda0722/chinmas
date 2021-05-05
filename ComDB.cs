using UnityEngine;
using System;
using System.Collections;
using LitJson;


public class ComDB : MonoBehaviour {

    public class User {
        public int id { get; set; }
        public string name { get; set; }
        public string area { get; set; }
        public double length { get; set; }
    }   
    public string url = "https://testapp001k.herokuapp.com/";

    public User sendData = new User ();
    public User recieveData = new User ();
    public int recieveNewID = 0;
        
    void Start() {
        sendData.id = 1;
        sendData.name = "pinpin";
        sendData.area = "兵庫県";
        sendData.length = 12.3;
        
        //StartCoroutine (ClearAll());
        //StartCoroutine (SetData(sendData));
        //StartCoroutine (GetData(1));
        StartCoroutine (GetNewID());
    }

    IEnumerator GetNewID()
    {
        WWWForm form = new WWWForm ();
        string post = "GetNewID";
        form.AddField (post, "dummy");
        using (WWW www = new WWW(url, form)) {
            yield return www;
            if (! string.IsNullOrEmpty (www.error)) {
                Debug.Log ("error:" + www.error);
                yield break;
            }
            Debug.Log ("text:" + www.text);
            recieveNewID = int.Parse(www.text);
            Debug.Log(recieveNewID);
        }
    }

    IEnumerator GetData(int id)
    {
        WWWForm form = new WWWForm ();
        string post = "GetData";
        form.AddField (post, id);
        using (WWW www = new WWW(url, form)) {
            yield return www;
            if (! string.IsNullOrEmpty (www.error)) {
                Debug.Log ("error:" + www.error);
                yield break;
            }
            Debug.Log ("text:" + www.text);
            recieveData = JsonMapper.ToObject<User>(www.text);
            Debug.Log("id:"+recieveData.id+", name:"+recieveData.name+", area:"+recieveData.area+", length:"+recieveData.length);
        }
    }

    IEnumerator SetData(User sendData)
    {
        WWWForm form = new WWWForm ();
        string post = "SetData";
        
        form.AddField (post, JsonMapper.ToJson(sendData));
        using (WWW www = new WWW(url, form)) {
            yield return www;
            if (! string.IsNullOrEmpty (www.error)) {
                Debug.Log ("error:" + www.error);
                yield break;
            }
            Debug.Log ("text:" + www.text);
        }
    }

    IEnumerator ClearAll()
    {
        WWWForm form = new WWWForm ();
        string post = "ClearAll";

        form.AddField (post, "dummy");
        using (WWW www = new WWW(url, form)) {
            yield return www;
            if (! string.IsNullOrEmpty (www.error)) {
                Debug.Log ("error:" + www.error);
                yield break;
            }
            Debug.Log ("text:" + www.text);
        }
    }
}