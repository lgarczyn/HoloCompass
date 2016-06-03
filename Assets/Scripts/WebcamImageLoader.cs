using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class WebcamImageLoader : MonoBehaviour {

	WebCamTexture cameraTexture;

	void Start()
	{
		List<WebCamDevice> devices = new List<WebCamDevice>(WebCamTexture.devices);

		WebCamDevice cam = devices.FirstOrDefault (x => x.isFrontFacing);

		string backCamName = cam.name;
		
		cameraTexture = new WebCamTexture(backCamName);

		GetComponent<RawImage>().texture = cameraTexture;
        GetComponent<RawImage>().enabled = true;

        cameraTexture.Play();

		//Handle no camera found
		//Handle warp
	}

	void Update()
	{
		//http://answers.unity3d.com/questions/773464/webcamtexture-correct-resolution-and-ratio.html#answer-1148424

		if ( cameraTexture.width < 100 )
		{
			return;
		}
		
		// change as user rotates iPhone or Android:
		
		int cwNeeded = cameraTexture.videoRotationAngle;
		// Unity helpfully returns the _clockwise_ twist needed
		// guess nobody at Unity noticed their product works in counterclockwise:
		int ccwNeeded = -cwNeeded;
		
		// IF the image needs to be mirrored, it seems that it
		// ALSO needs to be spun. Strange: but true.
		if ( cameraTexture.videoVerticallyMirrored )
			ccwNeeded += 180;

		RawImage rawImage = GetComponent<RawImage> ();
		RectTransform rawImageTransform = rawImage.rectTransform;
		AspectRatioFitter rawImageFitter = GetComponent<AspectRatioFitter> ();

		// you'll be using a UI RawImage, so simply spin the RectTransform
		rawImageTransform.localEulerAngles = new Vector3(0f,0f,ccwNeeded);
		
		float videoRatio = (float)cameraTexture.width/(float)cameraTexture.height;
		
		// you'll be using an AspectRatioFitter on the Image, so simply set it
		rawImageFitter.aspectRatio = videoRatio;
		
		// alert, the ONLY way to mirror a RAW image, is, the uvRect.
		// changing the scale is completely broken.
		if ( cameraTexture.videoVerticallyMirrored )
			rawImage.uvRect = new Rect(1,0,-1,1);  // means flip on vertical axis
		else
			rawImage.uvRect = new Rect(0,0,1,1);  // means no flip
		
		// devText.text =
		//  videoRotationAngle+"/"+ratio+"/"+wct.videoVerticallyMirrored;
	}
}
