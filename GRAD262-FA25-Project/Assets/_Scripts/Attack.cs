using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

//This script is attacked to external attacks like projectiles and spells.
public class Attack : MonoBehaviour
{
    public int damage;
    public string intendedTarget;
    public float knockBackForce;
    public float forceDelay = 0.15f;

    private void OnTriggerEnter(Collider other)
    {
        //Checks what is hit against what we wanted them to hit.
        //If the attack was successful, the intended target takes damage.
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
    
}
