using UnityEngine;

public static class MonoBehaviourExtensions
{
	static public RectTransform GetRectTransform(this MonoBehaviour mono)
	{
		return (mono.transform as RectTransform);
	}
}
