using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GraduationVR;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using Boo.Lang.Environments;
using SimpleJSON;
using System.Linq;

namespace GraduationVR
{
    public partial class Empty
    {
        [JsonProperty("records")]
        public List<Record> Records { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }
    }

    public partial class Record
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }

    public partial class Fields
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("isGraduating")]
        public string IsGraduating { get; set; }

        [JsonProperty("Player Model")]
        public List<PlayerModel> PlayerModel { get; set; }
    }

    public partial class PlayerModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    public class UI : MonoBehaviour
    {

        public TMP_InputField IDField;
        public Button JoinGame;
        public static string uiID;
        public TMP_InputField wrongID;

        public IEnumerator getPlayerModel(string id)
        {
            string api = "https://api.airtable.com/v0/appBtHGya4eSsk4Af/Table%201/";
            string appKey = System.IO.File.ReadAllText(@"C:\Users\Devrim\Desktop\appkey.txt");
            
            UnityWebRequest myWebRequest = new UnityWebRequest();
            myWebRequest.SetRequestHeader("Authorization", "Bearer " + appKey);
            myWebRequest.url = api;

            yield return myWebRequest.SendWebRequest();
            
            if (myWebRequest.isNetworkError || myWebRequest.isHttpError)
            {
                Debug.LogError(myWebRequest.error);
                yield break;
            }

            JSONNode webInfo = JSON.Parse(myWebRequest.downloadHandler.text);
            string myID = IDField.text;

            //Selects the correct person's part of the JSON based on the UI field's ID
            var record = webInfo["records"]["id"].Linq.FirstOrDefault(r => r.Id == myID);

            string playerID = webInfo["records"]["id"];
            string playerName = webInfo["records"]["fields"]["Name"];
            string playergraduating = webInfo["records"]["fields"]["isGraduting"];
            string playerModelURL = webInfo["records"]["fields"]["Player Model"]["url"];
            bool isGraduating;
            
            if (playergraduating == "No")
            {
                isGraduating = false;
            }
            else if (playergraduating == "Yes")
            {
                isGraduating = true;
            }


        }
        public void getText()
        {
            uiID = IDField.text;
            Debug.Log(uiID);
        }

        public void changeScene()
        {

        }
        
    }
}
