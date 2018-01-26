using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EPPZ.Networking
{


    using RestSharp;


	public class Networking : MonoBehaviour
	{


		void Start()
		{
			var client = new RestClient("http://maps.googleapis.com");
			var request = new RestRequest("maps/api/geocode/json", Method.GET);
			request.AddParameter("address", "Siofok");

			// Async (will be logged second).
			var asyncHandle = client.ExecuteAsync(request, asyncResponse =>
			{ Debug.Log("Async: "+asyncResponse.Content); });

			// Sync (will be logged first).
			var syncResponse = client.Execute(request);
			Debug.Log("Sync: "+syncResponse.Content);
		}
    }
}
