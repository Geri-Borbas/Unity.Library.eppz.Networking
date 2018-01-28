using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace EPPZ.Networking.Scenes
{


    using RestSharp;


	public class Requester : MonoBehaviour
	{


		public int amount = 10;
		public float delayBeforeFirstRequest = 1.0f;
		public float waitBetweenRequests = 1.0f;
		public float responseDelay = 0.5f;

		public enum RequestType { Unity, Sync, Async }
		public RequestType requestType = RequestType.Sync;

		IRestClient client;
		IRestRequest request;


		public void Fire()
		{ Invoke("StartRequests", delayBeforeFirstRequest); }

		void StartRequests()
		{
			// Create `RestSharp` classes.
			if (requestType != RequestType.Unity)
			{
				client = new RestClient("http://www.fakeresponse.com");
				request = new RestRequest("api", Method.GET);
				request.AddParameter("api_key", "edaadb03-b088-4ca1-a73d-2a5c646c686f");
				request.AddParameter("sleep", responseDelay.ToString());
			}

			for (int index = 0; index < amount; index++)
			{ Invoke(requestType.ToString(), index * waitBetweenRequests); }
		}

		void Unity()
		{ StartCoroutine(GetUnity()); }
 
    	IEnumerator GetUnity()
		{
			string URL = "http://www.fakeresponse.com/"		
				+"?api_key=edaadb03-b088-4ca1-a73d-2a5c646c686f"
				+"&sleep="+responseDelay.ToString();

        	UnityWebRequest unityWebRequest = UnityWebRequest.Get(URL);
        	yield return unityWebRequest.SendWebRequest();
 
        	if(unityWebRequest.isNetworkError)
			{ Debug.Log(unityWebRequest.error); }
        	else
			{ Debug.Log(unityWebRequest.downloadHandler.text); }
        }

		void Sync()
		{
			IRestResponse syncResponse = client.Execute(request);
			Debug.Log("Sync: "+syncResponse.Content);
		}

		void Async()
		{
			RestRequestAsyncHandle asyncHandle = client.ExecuteAsync(request, asyncResponse =>
			{ Debug.Log("Async: "+asyncResponse.Content); });
		}
    }
}
