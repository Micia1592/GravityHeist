using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLWithQuery : MonoBehaviour
{
    public string websiteURL = "https://oxfordeducation.eu.qualtrics.com/jfe/form/SV_5gUuE3gt6WWQrK6";
    public string queryString = "";
 

    void Start()
    {
        string fullURL = Application.absoluteURL;
        string[] splitURL = fullURL.Split(new[] { '?' }, 2);
        if (splitURL.Length > 1)
        {
            queryString = splitURL[1];
        }
        
        //string originaQueryString = PlayerPrefs.GetString("originalQueryString");

        //websiteURL += originaQueryString;

        //Debug.Log(Application.absoluteURL);
    }

    public void OpenUrl()
    {
        string urlWithQueryString = websiteURL +"?"+ queryString;

        Application.OpenURL(urlWithQueryString);

        //Application.OpenURL(websiteURL+"?"+queryString);
    }
}
