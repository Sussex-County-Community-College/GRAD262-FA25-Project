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

    //Takes in the game object that is applying knockback, the force of the knockback, and how long until that force is reduced to zero.
    //Publicly called by things like attacks and touching an enemy.
    public void KnockBackEffect(GameObject attacker, float knockBackForce, float forceDelay)
    {
        StopAllCoroutines(); //Prevents doubling up on knockback.

        //Determines the direction from the attacker and knocks current game object back.
        Vector3 direction = (transform.position - attacker.transform.position).normalized;
        rb.AddForce(direction * knockBackForce, ForceMode.Impulse);

        StartCoroutine(EndForce(forceDelay)); //Calls End Force
    }

    //Waits for a defined amount of time and then ends all force being applied to the game object from the knockback.
    private IEnumerator EndForce(float forceDelay)
    {
        yield return new WaitForSeconds(forceDelay);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
