using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    Coroutine shakeRoutine;
    Vector3 orignalPosition;

    private void Awake()
    {
        orignalPosition = transform.position;    
    }

    /// <summary>
    /// Shake the camera on x and y axis.
    /// </summary>
    /// <param name="duration">Length of shake in seconds.</param>
    /// <param name="xShake">Max distance shake will move on the x-axis.</param>
    /// <param name="yShake">Max distance shake will move on the y-axis.</param>
    [ExecuteInEditMode]
    public void ShakeCamera(CameraShakeConfig config)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(Shake(config.duration, config.xIntensity, config.yIntensity));
    }

    /// <summary>
    /// Stops the shaking coroutine and return camera to original start position.
    /// </summary>
    public void StopShaking()
    {
        if(shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        transform.position = orignalPosition;
    }

    public IEnumerator Shake(float duration, float xShake, float yShake)
    {
        orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * xShake * (1.0f - (elapsed / duration));
            float y = Random.Range(-1f, 1f) * yShake * (1.0f - (elapsed / duration));

            transform.position = new Vector3(orignalPosition.x + x, orignalPosition.y + y, orignalPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }
}