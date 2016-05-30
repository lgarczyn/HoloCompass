using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationTracker : MonoBehaviour
{
	public Text text;

	static public Vector3 position;
	static public Vector3 rotation;

	void Start()
	{
		//Use real GPS?
		Input.location.Start(0, 0);
	}

	void Update()
	{
		string log = 
			"Enabled " + Input.location.isEnabledByUser + "\r\n" +
			"Altitude " + Input.location.lastData.altitude + "\r\n" +
			"Latitude " + Input.location.lastData.latitude + " +- " + Input.location.lastData.horizontalAccuracy + "\r\n" +
			"Longitude " + Input.location.lastData.longitude + " +- " + Input.location.lastData.verticalAccuracy + "\r\n" +
			"Rotation " + Input.compass.trueHeading + " +- " + Input.compass.headingAccuracy + "\r\n";

		if (text)
			text.text = log;
		else
			Debug.Log (log);

		position.x = Input.location.lastData.longitude;
		position.y = Input.location.lastData.latitude;
		position.z = Input.location.lastData.altitude;
		rotation.z = Input.compass.trueHeading;
	}

}
