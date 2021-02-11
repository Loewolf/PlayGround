using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkScreen : MonoBehaviour
{
    public static DarkScreen instance;

    public float totalAppearingTime = 2f;
    public float waitingTime = 1f;
    public float totalFadingTime = 2f;
    public GameObject screen;
    public List<SmoothFade> darkScreenElements;
    private const float timeStep = 0.016666f;
    private IEnumerator darkScreenCoroutine;
    private float _totalAppearingTime;
    private float _totalFadingTime;
    private float f = 1f;
    private float appearingTimeStep;
    private float fadingTimeStep;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            darkScreenCoroutine = DarkScreenCoroutineRealtime(null);
            _totalAppearingTime = totalAppearingTime;
            _totalFadingTime = totalFadingTime;
            appearingTimeStep = 1f / _totalAppearingTime;
            fadingTimeStep = 1f / _totalFadingTime;
        }
        else
        {
            Debug.Log("Instance of DarkScreen " + " already exists");
            Destroy(this);
        }
    }

    private void Start()
    {
        foreach (SmoothFade element in darkScreenElements)
        {
            element.SetTotalAppearingAndFadingTime(_totalAppearingTime, _totalFadingTime);
        }
        StartCoroutine(darkScreenCoroutine);
    }

    public void ExecuteInDarkScreen(Action action, bool realtime = false)
    {
        screen.gameObject.SetActive(true);
        StopCoroutine(darkScreenCoroutine);
        darkScreenCoroutine = realtime? DarkScreenCoroutineRealtime(action) : DarkScreenCoroutine(action);
        StartCoroutine(darkScreenCoroutine);
    }

    private IEnumerator DarkScreenCoroutine(Action action)
    {
        while (f < 1f)
        {
            SetAlphaOnAppearingForElements(f);
            yield return null;
            f += Time.deltaTime * appearingTimeStep;
        }
        f = 1f;
        SetAlphaForElements(f);
        action?.Invoke();
        yield return new WaitForSeconds(waitingTime);
        while (f > 0f)
        {
            SetAlphaOnFadingForElements(f);
            yield return null;
            f -= Time.deltaTime * fadingTimeStep;
        }
        f = 0f;
        SetAlphaForElements(f);
        screen.gameObject.SetActive(false);
    }

    private IEnumerator DarkScreenCoroutineRealtime(Action action)
    {
        while (f < 1f)
        {
            SetAlphaOnAppearingForElements(f);
            yield return new WaitForSecondsRealtime(timeStep);
            f += timeStep * appearingTimeStep;
        }
        f = 1f;
        SetAlphaForElements(f);
        action?.Invoke();
        yield return new WaitForSecondsRealtime(waitingTime);
        while (f > 0f)
        {
            SetAlphaOnFadingForElements(f);
            yield return new WaitForSecondsRealtime(timeStep);
            f -= timeStep * fadingTimeStep;
        }
        f = 0f;
        SetAlphaForElements(f);
        screen.gameObject.SetActive(false);
    }

    private void SetAlphaOnAppearingForElements(float f)
    {
        foreach (SmoothFade element in darkScreenElements)
        {
            element.SetAlphaOnAppearing(f);
        }
    }

    private void SetAlphaForElements(float f)
    {
        foreach (SmoothFade element in darkScreenElements)
        {
            element.SetAlpha(f);
        }
    }

    private void SetAlphaOnFadingForElements(float f)
    {
        foreach (SmoothFade element in darkScreenElements)
        {
            element.SetAlphaOnFading(f);
        }
    }
}
