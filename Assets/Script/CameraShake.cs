using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class CameraShake : MonoBehaviour
{
    private float shakeTimeRemaining = 0f;
    private float shakeMagnitude = 0.3f;

    public IEnumerator Shake(float duration, float magnitude)
    {
        shakeTimeRemaining = duration;
        shakeMagnitude = magnitude;
        yield return null;
    }

    void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Nudge the camera from wherever CameraMove just put it
            transform.position += new Vector3(x, y, 0f);

            shakeTimeRemaining -= Time.deltaTime;
        }
    }
}