using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void KnockBackEffect(GameObject attacker, float knockBackForce, float forceDelay)
    {
        StopAllCoroutines();
        Vector3 direction = (transform.position - attacker.transform.position).normalized;
        rb.AddForce(direction * knockBackForce, ForceMode.Impulse);
        StartCoroutine(EndForce(forceDelay));
    }

    private IEnumerator EndForce(float forceDelay)
    {
        yield return new WaitForSeconds(forceDelay);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
