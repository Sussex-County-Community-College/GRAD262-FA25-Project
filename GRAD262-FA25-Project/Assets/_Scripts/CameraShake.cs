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
        originalPos = camTransform.localPosition; //Remembers the original position of the camera.
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeDuration > 0f) //Always ready to shake, just waiting for this value to be positive.
        {
            //Randomly changes the position of the camera within a set radius. 
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos; //When the shake is over, the camera returns to it's original position.
        }
    }

    //A public method for other things (like the earthquake spell) to call and add camera shake.
    //Takes in the duration of the shake and the amount of shake within the unit sphere.
    public void TriggerShake(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }
}
