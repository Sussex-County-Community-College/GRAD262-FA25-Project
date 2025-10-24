using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spells : MonoBehaviour
{
    public CameraShake camShake;

    [Header("Lightning Bolt")]
    public GameObject lightningStrike;
    public GameObject lightningCharge;
    public float lightningSpawnDistance;
    public float lightningDuration;
    public int lightningDamage;

    [Header("Fireball")]
    public GameObject fireball;
    public float fireSpawnDistance;
    public float fireDuration;
    public int fireDamage;

    [Header("Water Splash")]
    public GameObject waterSplash;
    public float waterSpawnDistance;
    public float waterDuration;
    public int waterDamage;

    [Header("Earthquake")]
    public GameObject earthquake;
    public float earthquakeSpawnDistance;
    public float EQShakeDuration;
    public float EQShakeAmount;
    public float earthquakeDuration;
    public int earthquakeDamage;

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
        newLightningStrike.GetComponent<Attack>().damage = lightningDamage;
        Destroy(newLightningStrike, lightningDuration);
    }
    public void Earthquake()
    {
        Vector3 spawnPos = transform.position + transform.up * earthquakeSpawnDistance;
        GameObject newEarthquake = Instantiate(earthquake, spawnPos, transform.rotation);
        newEarthquake.GetComponent<Attack>().damage = earthquakeDamage;
        camShake.TriggerShake(EQShakeDuration, EQShakeAmount);
        
        Destroy(newEarthquake, earthquakeDuration);
    }
    public void Fireball()
    {
        Vector3 spawnPos = transform.position + transform.forward * fireSpawnDistance;
        GameObject newfireball = Instantiate(fireball, spawnPos, transform.rotation);
        newfireball.GetComponent<Attack>().damage = fireDamage;
        Destroy(newfireball, fireDuration);
    }
    public void WaterSplash()
    {
        Vector3 spawnPos = transform.position + transform.forward * waterSpawnDistance;
        GameObject newWaterSplash = Instantiate(waterSplash, spawnPos, transform.rotation);
        newWaterSplash.GetComponent<Attack>().damage = waterDamage;
        Destroy(newWaterSplash, waterDuration);
    }

    

}
