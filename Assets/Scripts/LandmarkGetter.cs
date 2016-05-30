using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandmarkGetter : MonoBehaviour {

	static public List<Landmark> landmarks;
	public List<Landmark> landmarksGUI;

	static public event System.Action OnLandmarkUpdate;

	void Start ()
	{
		TextAsset urlAsset = Resources.Load("url") as TextAsset;
		
		StartCoroutine (WWWCoroutine(urlAsset.text));
	}

	IEnumerator WWWCoroutine (string url)
	{
		WWW www = new WWW (url);
		
		yield return(www);

		Debug.Log (www.isDone);
		
		string data = www.text;
		Debug.Log (data);

		landmarks = ReadData (data);
		landmarksGUI = landmarks;

		OnLandmarkUpdate ();
	}

	static List<Landmark> ReadData(string JSONData)
	{
		Debug.Log (JSONData);
		List<Landmark> landmarks = new List<Landmark> ();

		JSONObject data = new JSONObject (JSONData);

		data = data.GetField ("response");
		data = data.GetField ("data");

		foreach (JSONObject landmarkData in data.list)
		{
			Landmark landmark = new Landmark();
			
			landmark.name = landmarkData.GetField("name").str;
			if (landmarkData.HasField("category"))
				landmark.category = landmarkData.GetField("category").str;
			landmark.longitude = landmarkData.GetField("longitude").f;
			landmark.latitude = landmarkData.GetField("latitude").f;

			landmarks.Add(landmark);
		}
		return landmarks;
	}
}

[System.Serializable]
public struct Landmark
{
	public float latitude;
	public float longitude;
	public string name;
	public string category;
}
