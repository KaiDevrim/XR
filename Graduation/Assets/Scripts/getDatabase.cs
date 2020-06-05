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

        public static async Task<string> GetAsync(string baseId, string appKey, string api)
        {
            // Set the Headers
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + appKey);

            //Get the entire response from the API
            var response = await client.GetAsync(api);

            //Checks for if the api allows the request
            if (response.IsSuccessStatusCode)
            {
                Debug.Log("Success");

                //Save the response as a string to a variable called emptyResponse of type string
                var emptyReponse = await response.Content.ReadAsStringAsync();

                //Allows me to basically have the program understand each part of the API's JSON
                Empty empty = JsonConvert.DeserializeObject<Empty>(emptyReponse);

                Debug.Log(empty.Records[0].Fields.Name);

                //Saves the ID thats in the UI field to a variable
                

                    //Just a blanket return statement that will make it give back the current user's name
                    return empty.Records[0].Fields.Name;
               }


            else
            {
                //If the response code is not a success
                Debug.Log("Not Success");
                return "False";
            }


        }

        public async Task onClick()
        {
            string appKey = System.IO.File.ReadAllText(@"C:\Users\Devrim\Desktop\appkey.txt");
            string api = "https://api.airtable.com/v0/appBtHGya4eSsk4Af/Table%201/";
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

        public async Task Start()
        {
            //Get the appkey from a text file on my desktop
            //TODO implement a better system of getting this text file
            string key = System.IO.File.ReadAllText(@"C:\Users\Devrim\Desktop\appkey.txt");
            //Call the big boi method with the params of the baseID, the appkey, and the airtable url
            await GetAsync("appBtHGya4eSsk4Af", key, "https://api.airtable.com/v0/appBtHGya4eSsk4Af/Table%201/");
        }
    }
}