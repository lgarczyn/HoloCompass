using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LandmarkOverlay : MonoBehaviour
{
	GameObject landmarkPrefab;
	public List<RectTransform> landmarks;
	public List<Landmark> landmarkData;

	void Start ()
	{
		LandmarkGetter.OnLandmarkUpdate += UpdateLandmarks;	
	}

	void UpdateLandmarks ()
	{
		landmarks = new List<RectTransform>();
		landmarkData = new List<Landmark> (LandmarkGetter.landmarks);

		foreach (Landmark landmark in landmarkData)
		{
			RectTransform rt = GameObject.Instantiate(landmarkPrefab).GetComponent<RectTransform>();
			landmarks.Add(rt);
			rt.SetParent(transform, false);
		}
	}

	void Update()
	{
		Vector3 phonePos = LocationTracker.position;

		for (int i = 0; i < landmarks.Count(); i++)
		{
			Vector3 markPos = new Vector3(landmarkData[i].longitude, landmarkData[i].latitude);

			Vector3 diff = markPos - phonePos;


		}
	}
}
