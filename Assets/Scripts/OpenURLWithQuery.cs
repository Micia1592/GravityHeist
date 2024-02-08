using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLWithQuery : MonoBehaviour
{
    public string websiteURL = "https://oxfordeducation.eu.qualtrics.com/jfe/form/SV_5gUuE3gt6WWQrK6";
    public string queryString = "ID=${e://Field/PROLIFIC_PID}&?RID=${e://Field/ResponseID}";
    
    public void OpenUrl()
    {
        string urlWithQueryString = websiteURL + queryString;

        Application.OpenURL(urlWithQueryString);
    }
}
