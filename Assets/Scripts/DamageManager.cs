using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageManager : MonoBehaviour
{
    public static DamageManager Instance;

    [Header("Post Process")]
    public Volume volume;

    [Header("Fade")]
    public CanvasGroup fadePanel;

    [Header("Shake")]
    public Camera mainCamera;

    private Vignette vignette;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        volume.profile.TryGet(out vignette);

        fadePanel.alpha = 0;
    }

    public IEnumerator DamageSequence()
    {
        yield return StartCoroutine(FlashVignette());

        yield return StartCoroutine(CameraShake(0.25f, 0.2f));

        yield return StartCoroutine(FadeOut());

        PlayerHealth.Instance.Respawn();

        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FlashVignette()
    {
        float duration = 0.15f;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            vignette.intensity.value =
                Mathf.Lerp(0.3f, 1f, t / duration);

            yield return null;
        }

        t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            vignette.intensity.value =
                Mathf.Lerp(1f, 0.3f, t / duration);

            yield return null;
        }
    }

    IEnumerator CameraShake(float duration, float strength)
    {
        Vector3 originalPos = mainCamera.transform.localPosition;

        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float x = Random.Range(-strength, strength);
            float y = Random.Range(-strength, strength);

            mainCamera.transform.localPosition =
                originalPos + new Vector3(x, y, 0);

            yield return null;
        }

        mainCamera.transform.localPosition = originalPos;
    }

    IEnumerator FadeOut()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;

            fadePanel.alpha = t;

            yield return null;
        }

        fadePanel.alpha = 1;
    }

    IEnumerator FadeIn()
    {
        float t = 1;

        while (t > 0)
        {
            t -= Time.deltaTime * 2f;

            fadePanel.alpha = t;

            yield return null;
        }

        fadePanel.alpha = 0;
    }
}