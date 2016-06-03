using UnityEngine;
using System.Collections;

public class CameraGyroController : MonoBehaviour {
    
	void LateUpdate ()
    {
         GetComponent<Camera>().transform.localRotation = Input.gyro.attitude;
    }
}
