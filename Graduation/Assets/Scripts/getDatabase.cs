using System;
using AirtableApiClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UnityEngine;

public class getDatabase : MonoBehaviour
{
    

    static void Main(string[] args)
    {
       
    }

    public async Task<AirtableRetrieveRecordResponse> RetrieveRecord(string tableName, string id)
    {
        string baseId = "base_ID";
        string appKey = "app_KEY";
        tableName = "Hackers";
        id = "ID";

        using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
        {
            Task<AirtableRetrieveRecordResponse> task = airtableBase.RetrieveRecord(tableName, id);
            var response = await task;
            if (!response.Success)
            {
                string errorMessage = null;
                
                if (response.AirtableApiError is AirtableApiException)
                {
                    errorMessage = response.AirtableApiError.ErrorMessage;
                    Console.WriteLine(errorMessage);
                    
                }
                else
                {
                    errorMessage = "Unknown error";
                    Console.WriteLine(errorMessage);
                }
                // Report errorMessage
            }
            else
            {
                var record = response.Record;
                // Do something with your retrieved record.
                // Such as getting the attachmentList of the record if you
                // know the Attachment field name
                var attachmentList = response.Record.GetAttachmentField("Name");
            }
            Console.WriteLine(response);
            return response;

        }
        
        Console.WriteLine("OK 2");
    }
}

//
//