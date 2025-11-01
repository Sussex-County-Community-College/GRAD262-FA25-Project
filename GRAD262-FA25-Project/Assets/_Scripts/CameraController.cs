using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xDistance, zDistance, duration;
    public Transform playerTransform;

    private CameraShake camShake;

    private void Start()
    {
        camShake = GetComponent<CameraShake>();
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(playerTransform.position);
        if (screenPos.x < 0)
            StartCoroutine(MoveCam(-xDistance, 0));
        if(screenPos.x > 1)
            StartCoroutine(MoveCam(xDistance, 0));
        if (screenPos.y < 0)
            StartCoroutine(MoveCam(0, -zDistance));
        if (screenPos.y > 1)
            StartCoroutine(MoveCam(0, zDistance));
            
    }

    public IEnumerator MoveCam(float xDis, float zDis)
    {
        Vector3 endPos = new Vector3(transform.position.x + xDis, transform.position.y, transform.position.z + zDis);
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(transform.position, endPos, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        camShake.SetOriginalPos(endPos);
    }
}
