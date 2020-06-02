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

namespace GraduationVR
{
    public class getDatabase : MonoBehaviour
    {
            string baseId = "appBtHGya4eSsk4Af";
            string appKey = System.IO.File.ReadAllText(@"C:\Users\Devrim\Desktop\appkey.txt");
            HttpClient client = new HttpClient();
            protected string api = "https://api.airtable.com/v0/appBtHGya4eSsk4Af/Table%201/recKg4RTxeXjQWVbC";

        public string Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
            var headers = request.Headers[$"Authorization: Bearer {appKey}"];
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public void Awake()
        {
            //var responseString = await client.GetStringAsync("http://www.example.com/recepticle.aspx");
            /*var task = RetrieveRecord("Hackers", "recKg4RTxeXjQWVbC");
            Console.WriteLine("This is the task1" + task);
            Console.WriteLine("This is the task2" + task.Result);*/


        }
    }
}

//
//