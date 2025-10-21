using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spells : MonoBehaviour
{
    public CameraShake camShake;

    [Header("Lightning Bolt")]
    public GameObject lightningStrike;
    public float lightningSpawnDistance;
    public float lightningDuration;

    [Header("Fireball")]
    public GameObject fireball;
    public float fireSpawnDistance;
    public float fireDuration;

    [Header("Water Splash")]
    public GameObject waterSplash;
    public float waterSpawnDistance;
    public float waterDuration;

    [Header("Earthquake")]
    public GameObject earthquake;
    public float earthquakeSpawnDistance;
    public float EQShakeDuration;
    public float EQShakeAmount;
    public float earthquakeDuration;

    public void LightningStrike()
    {
        Debug.Log("You casted Lightning Strike!");
        Vector3 spawnPos = transform.position + transform.forward * lightningSpawnDistance;
        Quaternion lightningRotation = new Quaternion(0, 0, 0, 0);
        GameObject newLightningStrike = Instantiate(lightningStrike, spawnPos, lightningRotation);
        Destroy(newLightningStrike, lightningDuration);
    }
    public void Earthquake()
    {
        Debug.Log("You casted Earthquake!");
        Vector3 spawnPos = transform.position + transform.up * earthquakeSpawnDistance;
        GameObject newEarthquake = Instantiate(earthquake, spawnPos, transform.rotation);
        camShake.TriggerShake(EQShakeDuration, EQShakeAmount);
        Destroy(newEarthquake, earthquakeDuration);
    }
    public void Fireball()
    {
        Debug.Log("You casted Fireball!");
        Vector3 spawnPos = transform.position + transform.forward * fireSpawnDistance;
        GameObject newfireball = Instantiate(fireball, spawnPos, transform.rotation);
        Destroy(newfireball, fireDuration);
    }
    public void WaterSplash()
    {
        Debug.Log("You casted Water Splash!");
        Vector3 spawnPos = transform.position + transform.forward * waterSpawnDistance;
        GameObject newWaterSplash = Instantiate(waterSplash, spawnPos, transform.rotation);
        Destroy(newWaterSplash, waterDuration);
    }

}
