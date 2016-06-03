using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandmarkGetter : MonoBehaviour {

	static public List<Landmark> landmarks;
	public List<Landmark> landmarksGUI;

	static public event System.Action<List<Landmark>> OnLandmarkUpdate;

	void Start ()
	{
		TextAsset urlAsset = Resources.Load("url") as TextAsset;

		string url = urlAsset.text;

		url = url.Replace ("<KEY>", "lkoi2xh39wNrb7DbCF7ENlp1Q5R8hs3K7fEjjfwv");
		url = url.Replace ("<LAT>", LocationTracker.latitude.ToString());
		url = url.Replace ("<LON>", LocationTracker.longitude.ToString());

		Debug.Log (url);
		StartCoroutine (WWWCoroutine(url));
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

		OnLandmarkUpdate (landmarks);
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
