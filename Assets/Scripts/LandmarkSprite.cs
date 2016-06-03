using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LandmarkSprite : MonoBehaviour
{

	static HashSet<LandmarkSprite> sprites = new HashSet<LandmarkSprite>();

	public GameObject LabelPrefab;
    float lastForce = 0;

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
			Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, transform.position);
            Vector2 idealPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(label.transform.parent as RectTransform, screenPos, mainCamera, out idealPos);
            Vector2 currentPos = label.rectTransform.anchoredPosition;

            Rect labelRect = label.rectTransform.rect;
            labelRect.position = label.rectTransform.anchoredPosition;

            float force = (idealPos.y - currentPos.y) / 100;

			foreach(LandmarkSprite other in sprites)
			{
                if (other == this)
                    continue;
				Rect otherRect = other.label.rectTransform.rect;
                otherRect.position = other.label.rectTransform.anchoredPosition;
                if (labelRect.Overlaps(otherRect))
				{
                    if (labelRect.center.y > otherRect.center.y)
                    {
                        force += (otherRect.yMax - labelRect.yMin) / 10;
                    }
                    else if (labelRect.center.y < otherRect.center.y)
                    {
                        force += (otherRect.yMin - labelRect.yMax) / 10;
                    }
                    else
                    {
                        force += Random.Range(-1f, 1f);
                    }
				}
			}
            Vector2 newPos = new Vector2(idealPos.x, currentPos.y + force);

            label.rectTransform.anchoredPosition = newPos;
		}
	}

	void OnDestroy()
	{
		if (label) {
			sprites.Remove (this);
		}
	}
}