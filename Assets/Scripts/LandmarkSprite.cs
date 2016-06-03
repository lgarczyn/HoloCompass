using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LandmarkSprite : MonoBehaviour
{

	static HashSet<LandmarkSprite> sprites = new HashSet<LandmarkSprite>();

	public GameObject LabelPrefab;

	Camera mainCamera;
	Text label;

	public void Setup(string name, RectTransform parent, Camera camera)
	{
		label = Instantiate(LabelPrefab).GetComponent<Text>();
		label.transform.SetParent (parent, false);
		label.text = name;

		mainCamera = camera;

		
		sprites.Add(this);
	}

	void Update ()
	{
		if (label)
		{
			Vector2 anchor = label.rectTransform.anchoredPosition;

			anchor.x = RectTransformUtility.WorldToScreenPoint(mainCamera, transform.position).x;

			float force = -label.rectTransform.anchoredPosition.y / 100;

			foreach(LandmarkSprite other in sprites)
			{
				Rect labelRect = label.rectTransform.rect;
				Rect otherRect = other.label.rectTransform.rect;
				if (labelRect.Overlaps(otherRect))
				{
					force += (labelRect.center.y > otherRect.center.y) ? 4 : -4;
				}
			}
			label.rectTransform.anchoredPosition += new Vector2(0, force);
		}
	}

	void OnDestroy()
	{
		if (label) {
			sprites.Remove (this);
		}
	}
}