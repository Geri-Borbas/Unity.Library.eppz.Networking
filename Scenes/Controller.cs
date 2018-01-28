using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EPPZ.Networking.Scenes
{


	public class Controller : MonoBehaviour
	{

		

		public Requester[] requesters;
		public Text text;
		public LagMeter lagMeter;
		public float duration = 10.0f;

		int count;


		void Start()
		{ InvokeRepeating("Restart", 0.0f, duration); }
		
		void Restart()
		{
			// Fire next `Requester`.
			int index = count % requesters.Length;
			requesters[index].Fire();
			count++;

			// Show `Requester` type.
			text.text = requesters[index].requestType.ToString();

			// Restart lag meter.
			lagMeter.Restart();
		}
	}
}
