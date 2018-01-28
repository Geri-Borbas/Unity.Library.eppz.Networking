using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EPPZ.Networking.Scenes
{


	using System;
	using System.Diagnostics;
	using UnityEngine.Serialization;


	public class LagMeter : MonoBehaviour
	{


		// Properties.
		public Color backgroundColor;
		[FormerlySerializedAs("ledColor")]
		public Color tickColor;
		[FormerlySerializedAs("ledScale")]
		public Vector3 tickScale;
		public int steps = 10; // Steps per second.
		public int interval = 10; // Seconds total

		// UI.
		public MeshFilter backgroundMeshFilter;
		Mesh _backgroundMesh;
		Color[] _backgroundColors;
		Mesh _tickMesh;
		Color[] _tickColors;

		// Time measurement.
		Stopwatch stopWatch;
		bool started;
		long elapsedMilliseconds;
		long latestMilliseconds;
		long elapsedSteps;
		long latestSteps;

		// Tick objects.
		[FormerlySerializedAs("ledPrototype")]
		public GameObject tickObjectPrototype;
		GameObject _tickObject;
		Transform _tickObjectTransform;
		Vector3 _tickObjectPosition;
		List<GameObject> _tickObjects = new List<GameObject>();


		void Start()
		{ Restart(); }
				
		public void Restart()
		{
			// Reference prototype.
			_tickObject = tickObjectPrototype;

			// Flush tick objects.
			foreach (GameObject eachTickObject in _tickObjects)
			{ DestroyImmediate(eachTickObject); }
			_tickObjects.Clear();

			// Reset times.
			elapsedMilliseconds = 0;
			latestMilliseconds = 0;
			elapsedSteps = 0;
			latestSteps = 0;

			// Start stopwatch.
			if (stopWatch != null) stopWatch.Stop();
			stopWatch = new Stopwatch();
			stopWatch.Start();
			started = true;

			CacheTickObject();

			// Adjust background mesh colors.			
			_backgroundMesh = backgroundMeshFilter.mesh;
			_backgroundColors = new Color[_backgroundMesh.vertexCount];
			for (int index = 0; index < _backgroundColors.Length; index++)
			{ _backgroundColors[index] = backgroundColor; }
			_backgroundMesh.colors = _backgroundColors;
		}

		void Update()
		{
			if (started == false) return;

			// Get elapsed time span.
			TimeSpan timeSpan = stopWatch.Elapsed;
			elapsedMilliseconds =
				// timeSpan.Days * 24 * 60 * 60 * 1000 + 
                // timeSpan.Hours * 60 * 60 * 1000 + 
                // timeSpan.Minutes * 60 * 1000 + 
                timeSpan.Seconds * 1000 + 
                timeSpan.Milliseconds;
			elapsedSteps = elapsedMilliseconds / (1000 / steps);

			// Only if a decisecond elapsed.
			if (latestSteps >= elapsedSteps) return;

			Tick();

			latestSteps = elapsedSteps;
		}

		void CacheTickObject()
		{
			_tickObjectTransform = _tickObject.GetComponent<Transform>();
			_tickObjectPosition = _tickObjectTransform.position;
			_tickMesh = _tickObject.GetComponent<MeshFilter>().mesh;
			_tickColors = new Color[_tickMesh.vertexCount];
		}

		void Tick()
		{
			// Get delta
			float deltaMilliseconds = elapsedMilliseconds - latestMilliseconds;
			latestMilliseconds = elapsedMilliseconds;

			// Calculate mesh color (delta to step ratio).
			float percent = deltaMilliseconds / (float)(1000 / steps);
			float alpha = percent / (float)steps;
			Color color = new Color(tickColor.r, tickColor.g, tickColor.b, alpha);

			// Clone.
			_tickObject = GameObject.Instantiate(_tickObject);
			_tickObjects.Add(_tickObject);
			_tickObject.name = "Tick ("+elapsedSteps+"."+deltaMilliseconds+")";
			CacheTickObject();
			_tickObjectTransform.localScale = new Vector3(tickScale.x, tickScale.y * percent, tickScale.z);

			// Adjust mesh color.
			for (int index = 0; index < _tickColors.Length; index++)
			{ _tickColors[index] = color; }
			_tickMesh.colors = _tickColors;

			// Calculate / adjust mesh position.
			_tickObjectPosition.y = 1.0f * ((float)elapsedMilliseconds / (float)(1000 / steps));
			_tickObjectPosition.y -= _tickObjectTransform.localScale.y * 0.5f; // Shift up
			_tickObjectTransform.position = _tickObjectPosition;
		}
	}
}
