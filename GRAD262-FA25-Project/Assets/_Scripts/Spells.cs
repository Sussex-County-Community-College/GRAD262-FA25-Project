using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spells : MonoBehaviour
{
    public CameraShake camShake;

    private Animator animator;

    [Header("Lightning Bolt")]
    public GameObject lightningStrike;
    public GameObject lightningCharge;
    public float lightningSpawnDistance;
    public float lightningDuration;
    public int lightningDamage;
    public int lightningManaCost;

    [Header("Fireball")]
    public GameObject fireball;
    public float fireSpawnDistance;
    public float fireDuration;
    public int fireDamage;
    public int fireManaCost;
    public float fireAnimDuration;
    public float fireSpellOffset;

    [Header("Water Splash")]
    public GameObject waterSplash;
    public float waterSpawnDistance;
    public float waterDuration;
    public int waterDamage;
    public int waterManaCost;
    public float waterAnimDuration;
    public float waterSpellOffset;

    [Header("Earthquake")]
    public GameObject earthquake;
    public float earthquakeSpawnDistance;
    public float EQShakeDuration;
    public float EQShakeAmount;
    public float earthquakeDuration;
    public int earthquakeDamage;
    public int earthquakeManaCost;
    public float earthquakeAnimDuration;
    public float earthquakeSpellOffset;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CastFireball()
    {
        animator.SetTrigger("fireAttack");
        StartCoroutine(Fireball());
    }

    public void CastLightningStrike()
    {
        animator.SetTrigger("lightningAttack");
        //Lightning Strike
    }

    public void CastWaterSplash()
    {
        animator.SetTrigger("waterAttack");
        StartCoroutine (WaterSplash());
    }

    public void CastEarthquake()
    {
        animator.SetTrigger("earthAttack");
        StartCoroutine(Earthquake());
    }

    public GameObject LightingChargeStart()
    {
        Vector3 spawnPos = transform.position + transform.forward * lightningSpawnDistance;
        Quaternion spawnRot = new Quaternion(0, 0, 90, 0);
        GameObject newlightningCharge = Instantiate(lightningCharge, spawnPos, spawnRot);
        return lightningCharge;
    }
    
    public void LightningStrike()
    {
        Vector3 spawnPos = transform.position + transform.forward * lightningSpawnDistance;
        Quaternion lightningRotation = new Quaternion(0, 0, 0, 0);
        GameObject newLightningStrike = Instantiate(lightningStrike, spawnPos, lightningRotation);
        DefineAttack(newLightningStrike, lightningDamage);
        Destroy(newLightningStrike, lightningDuration);

    }
    public IEnumerator Earthquake()
    {
        yield return new WaitForSeconds(earthquakeSpellOffset);
        Vector3 spawnPos = transform.position + transform.up * earthquakeSpawnDistance;
        GameObject newEarthquake = Instantiate(earthquake, spawnPos, transform.rotation);
        DefineAttack(newEarthquake, earthquakeDamage);
        camShake.TriggerShake(EQShakeDuration, EQShakeAmount);
        Destroy(newEarthquake, earthquakeDuration);
    }
    private IEnumerator Fireball()
    {
        yield return new WaitForSeconds(fireSpellOffset);
        Vector3 spawnPos = transform.position + transform.forward * fireSpawnDistance;
        GameObject newfireball = Instantiate(fireball, spawnPos, transform.rotation);
        DefineAttack(newfireball, fireDamage);
        Destroy(newfireball, fireDuration);
    }
    public IEnumerator WaterSplash()
    {
        yield return new WaitForSeconds(waterSpellOffset);
        Vector3 spawnPos = transform.position + transform.forward * waterSpawnDistance;
        GameObject newWaterSplash = Instantiate(waterSplash, spawnPos, transform.rotation);
        DefineAttack(newWaterSplash, waterDamage);
        Destroy(newWaterSplash, waterDuration);
    }

    private void DefineAttack(GameObject spell, int damage)
    {
        Attack attack = spell.GetComponent<Attack>();
        attack.damage = damage;
        attack.intendedTarget = "Enemy";
    }
}
