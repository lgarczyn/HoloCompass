using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LandmarksDisplay : MonoBehaviour
{
	public List<Transform> landmarks;

	public Camera camera;
	public GameObject prefab;
	public Vector3 rotOffset;

	void Start ()
	{
		LandmarkGetter.OnLandmarkUpdate += UpdateLandmarks;	
	}

	void UpdateLandmarks (List<Landmark> landmarkData)
	{
		camera.transform.position = GetWorldCoordinates(landmarkData [0].longitude, landmarkData [0].latitude);

		landmarks = new List<Transform>();

		foreach (Landmark landmark in landmarkData)
		{
			Transform t = GameObject.Instantiate(prefab).GetComponent<Transform>();
			landmarks.Add(t);
			t.SetParent(transform, false);

			t.transform.position = GetWorldCoordinates(landmark.longitude, landmark.latitude);

		}
	}

	void Update()
	{
		camera.transform.position = GetWorldCoordinates (LocationTracker.longitude, LocationTracker.altitude, LocationTracker.latitude);
		camera.transform.rotation = Quaternion.Euler (LocationTracker.rotation + rotOffset);
	}


	static public Vector3 GetWorldCoordinates(float longitude, float latitude, float altitude = 0)
	{
		return new Vector3 (longitude, altitude, latitude) * 1000;
	}
}
