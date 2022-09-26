using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class DownloadGoogleSheet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
        using var client = new WebClient();
        client.Encoding = Encoding.UTF8;
        client.Headers.Add("accept", "*/*");

        string url = "https://docs.google.com/spreadsheets/export?id=1Mug-SjtXDXWj6oWzXhZp69LppHIDfclRhNWXiOqtSaI&exportFormat=csv";
        //byte[] outputData= client.DownloadData(url);
        string outputData= client.DownloadString(url);

        List<Dictionary<string, object>> data_Sample = CSVReader.Read(outputData);

        for (int i = 0; i < data_Sample.Count; i++)
        {
            print(data_Sample[i]["Name"].ToString());
        }
    }


}
