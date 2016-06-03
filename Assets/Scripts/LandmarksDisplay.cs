using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LandmarksDisplay : MonoBehaviour
{
	public List<LandmarkSprite> landmarks;

	public Camera mainCamera;
	public RectTransform labelParent;
	public GameObject spritePrefab;

	void Start ()
	{
		LandmarkGetter.OnLandmarkUpdate += UpdateLandmarks;	
	}

	void UpdateLandmarks (List<Landmark> landmarkData)
	{
		mainCamera.transform.position = GetWorldCoordinates(landmarkData [0].longitude, landmarkData [0].latitude);

		landmarks = new List<LandmarkSprite>();

		foreach (Landmark landmark in landmarkData)
		{
			LandmarkSprite sprite = Instantiate(spritePrefab).GetComponent<LandmarkSprite>();

			sprite.transform.SetParent(transform, false);
			sprite.transform.position = GetWorldCoordinates(landmark.longitude, landmark.latitude);

			sprite.Setup(landmark.name, labelParent, mainCamera);

			landmarks.Add(sprite);
		}
	}

	void LateUpdate()
	{
		mainCamera.transform.position = GetWorldCoordinates (LocationTracker.longitude, LocationTracker.latitude, LocationTracker.altitude);
	}


	static public Vector3 GetWorldCoordinates(float longitude, float latitude, float altitude = 0)
	{
		return new Vector3 (longitude, altitude, latitude) * 1000;
	}
}
