using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudioSource : MonoBehaviour
{
	public static IEnumerator StartFade(AudioSource source, float duration, float targetVol) {
		float currentTime = 0;
		float start = source.volume;

		while (currentTime < duration) {
			currentTime += Time.deltaTime;
			source.volume = Mathf.Lerp(start, targetVol, currentTime / duration);
			yield return null;

		}

		yield break;

	}
}
