using System;
using AirtableApiClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UnityEngine;
using GraduationVR;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        [JsonProperty("createdTime")]
        public DateTimeOffset CreatedTime { get; set; }
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

    public class getDatabase : MonoBehaviour
    {
        
        static HttpClient client = new HttpClient();
        public static string theScene = "MainScene";
        public Button JoinGame;

        public static async Task onClick(string appKey, string api)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + appKey);
            var response = await client.GetAsync(api);

            if (response.IsSuccessStatusCode)
            {
                var emptyReponse = await response.Content.ReadAsStringAsync();
                Empty empty = JsonConvert.DeserializeObject<Empty>(emptyReponse);

                string myID = UI.uiID;

                //Selects the correct person's part of the JSON based on the UI field's ID
                var record = empty.Records.FirstOrDefault(r => r.Id == myID);

                //If the above is unable to find the person then it will return null
                if (record != null)
                {
                    //Get the URL of the playermodel
                    var remoteUri = record.Fields.PlayerModel[0].Url;
                    //Set the name of the playermodel to person's id
                    string fileName = record.Id;
                    //Sets the file path to the path of the sln/

                    //TODO Update this comment to include the actual path of the fbx
                    string FilePath = Directory.GetCurrentDirectory() + "/Models/" + fileName + ".fbx";

                    //Make a new WebClient object called myWebClient
                    using (WebClient myWebClient = new WebClient())
                    {
                        //Check if the directory called Models exists
                        if (!Directory.Exists("Models"))
                        {
                            //If the directory does not exist then make it.
                            Directory.CreateDirectory("Models");
                        }
                        //If it does exist
                        else
                        {
                            //Wait for the download of the selected user's model from the airtable download and put it in the correct file path
                            await myWebClient.DownloadFileTaskAsync(remoteUri, FilePath);
                            SceneManager.LoadScene(theScene);
                            //Put in the console the url for the player model url
                            Debug.Log(remoteUri);
                        }
                    }
                }
            }
        }
        public static void notOnClick()
        {
            string key = System.IO.File.ReadAllText(@"C:\Users\Devrim\Desktop\appkey.txt");
            onClick(key, "https://api.airtable.com/v0/appBtHGya4eSsk4Af/Table%201/");
        }

    }
}