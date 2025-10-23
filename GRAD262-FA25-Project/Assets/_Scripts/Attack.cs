using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public string tagOnImpact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(damage);
        }
            
    }
}
