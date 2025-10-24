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
        if (other.CompareTag(tagOnImpact))
        {
            if(tagOnImpact == "Enemy")
            {
                EnemyAI enemy = other.GetComponent<EnemyAI>();
                enemy.TakeDamage(damage);
            }
            if(tagOnImpact == "Player")
            {
                PlayerControls player = other.GetComponent<PlayerControls>();
                player.TakeDamage(damage);
            }
        }
            
    }
}
