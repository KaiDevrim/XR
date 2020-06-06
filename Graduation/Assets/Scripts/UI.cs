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
using System.IO;

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
        //public TMP_InputField wrongID;

        public IEnumerator getDatabase()
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

 
            Empty empty = JsonConvert.DeserializeObject<Empty>(webInfo);
            var record = empty.Records.FirstOrDefault(r => r.Id == uiID);

            bool isGraduating;
            
            if (empty.Records[0].Fields.IsGraduating == "No")
            {
                isGraduating = false;
            }
            else if (empty.Records[0].Fields.IsGraduating == "Yes")
            {
                isGraduating = true;
            }

            if (record != null)
            {
                Debug.Log(empty.Records[0].Fields.Name);

                UnityWebRequest getModel = new UnityWebRequest(empty.Records[0].Fields.PlayerModel[0].Url);
                getModel.SetRequestHeader("Authorization", "Bearer " + appKey);
                //Get the URL of the playermodel
                var remoteUri = record.Fields.PlayerModel[0].Url;

                //TODO Update this comment to include the actual path of the fbx
                string FilePath = Path.Combine(Application.persistentDataPath, empty.Records[0].Fields.PlayerModel[0].Id + ".fbx");
                getModel.downloadHandler = new DownloadHandlerFile(FilePath);

                yield return getModel.SendWebRequest();
                if (getModel.isNetworkError || getModel.isHttpError)
                {
                    Debug.LogError(getModel.error);
                }
                else
                {
                    Debug.Log("File successfully downloaded and saved to " + FilePath);
                    SceneManager.LoadScene("MainScene");
                }
            }

            else
            {
                Debug.Log("Record is null");
            }

        }

        public void getText()
        {
            uiID = IDField.text;
            Debug.Log(uiID);
        }

        public void onClick()
        {
            StartCoroutine(getDatabase());
        }
        
    }
}
