using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;

    private void Awake()
    {
        if(camTransform == null)
        {
            camTransform = GetComponent(typeof(Camera)) as Transform;
        }
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeDuration > 0f)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void TriggerShake(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }
}
