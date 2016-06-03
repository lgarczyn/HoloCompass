using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationTracker : MonoBehaviour
{
	public Text text;

	static public float longitude;
	static public float latitude;
	static public float altitude;
	static public float rotation;
	static public Quaternion attitude;

	void Awake()
	{
		//Use real GPS?
		Input.location.Start(0, 0);
		Input.gyro.enabled=true;
		Update ();
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

		longitude = Input.location.lastData.longitude;
		latitude = Input.location.lastData.latitude;
		altitude = Input.location.lastData.altitude;
		rotation = Input.compass.trueHeading;
		attitude = Input.gyro.attitude;

		latitude = 43.2641532f;
		longitude = -0.5886227f;
	}

}
