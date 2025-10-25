using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public string intendedTarget;
    public float knockBackForce;
    public float forceDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(intendedTarget))
        {
            if(intendedTarget == "Enemy")
            {
                EnemyAI enemy = other.GetComponent<EnemyAI>();
                enemy.TakeDamage(damage);
            }
            if(intendedTarget == "Player")
            {
                PlayerControls player = other.GetComponent<PlayerControls>();
                player.TakeDamage(damage, gameObject, knockBackForce, forceDelay);
            }
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(intendedTarget))
        {
            if (intendedTarget == "Enemy")
            {
                EnemyAI enemy = collision.collider.GetComponent<EnemyAI>();
                enemy.TakeDamage(damage);
            }
            if (intendedTarget == "Player")
            {
                PlayerControls player = collision.collider.GetComponent<PlayerControls>();
                player.TakeDamage(damage, gameObject, knockBackForce, forceDelay);
            }
        }

    }
}
