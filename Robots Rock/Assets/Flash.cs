using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public Renderer[] myRenderers;

    [Range(0.1f, 1.0f)]
    public float flashIntensity;
    public Color flashColor;

    private Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
    private Coroutine flashRoutine;

    private void OnEnable()
    {
        foreach (Renderer r in myRenderers)
        {
            foreach (Material m in r.materials)
            {
                originalColors.Add(m, m.color);
            }
        }
    }

    public void StartFlashing(float duration)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine(duration));
    }

    IEnumerator FlashRoutine(float duration)
    {
        foreach (Renderer r in myRenderers)
        {
            foreach (Material mat in r.materials)
            {
                mat.color = Color.Lerp(mat.color, flashColor, 0.5f);
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (Renderer r in myRenderers)
        {
            foreach (Material mat in r.materials)
            {
                mat.color = originalColors[mat];
            }
        }
    }
}
