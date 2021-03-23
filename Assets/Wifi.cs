using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
// UnityWebRequest.Get example
// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.
public class Wifi : MonoBehaviour
{
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        //LED off
        StartCoroutine(GetRequest("http://192.168.0.158/L"));
        Debug.Log("Reset everything");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(GetRequest("http://192.168.0.158/H"));
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            Debug.Log("mouse down - colour red/ led on");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(GetRequest("http://192.168.0.158/L"));
            gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            Debug.Log("mouse down - colour black/ led off");
        }

    }

 //   private void OnMouseDown()
   // {
   //
     //   StartCoroutine(GetRequest("http://192.168.0.158/H"));
       // gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        //Debug.Log("mouse down - colour red/ led on");
    //}

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}