using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable] public class CameraShakeEvent : UnityEvent<float, float> { };

public class CameraShake : MonoBehaviour
{
    Coroutine shakeRoutine;

    [ExecuteInEditMode]
    public void ShakeCamera(float d, float m)
    {
        if(shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(Shake(d, m));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude * (1.0f - (elapsed / duration));
            float y = Random.Range(-1f, 1f) * magnitude * (1.0f - (elapsed / duration));

            transform.position = new Vector3(orignalPosition.x + x, orignalPosition.y + y, orignalPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }
}