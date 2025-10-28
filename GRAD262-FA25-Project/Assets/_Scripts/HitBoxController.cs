using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for controlling the collider of a game object.
//For example: The earthquake's collider should deactivate after a short time.
public class HitBoxController : MonoBehaviour
{
    public float hitBoxDuration;

    private SphereCollider hitBox;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<SphereCollider>();
        StartCoroutine(TurnOffHitBox());
    }

    private IEnumerator TurnOffHitBox()
    {
        yield return new WaitForSeconds(hitBoxDuration);
        hitBox.enabled = false;
    }
}
