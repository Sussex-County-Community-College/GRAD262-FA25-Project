using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spells : MonoBehaviour
{
    [Header("Lightning Bolt")]
    public GameObject lightningStrike;
    public float lightningSpawnDistance = 5;

    [Header("Fireball")]
    public GameObject fireball;
    public float fireSpawnDistance = 1f;

    [Header("Water Splash")]
    public GameObject waterSplash;
    public float waterSpawnDistance = 1f;
    public float waterDuration = 2f;


    public void LightningStrike()
    {
        Debug.Log("You casted Lightning Strike!");
        Vector3 spawnPos = transform.position + transform.forward * lightningSpawnDistance;
        GameObject newLightningStrike = Instantiate(lightningStrike, spawnPos, transform.rotation);
        Destroy(newLightningStrike, 1f);
    }
    public void Earthquake()
    {
        Debug.Log("You casted Earthquake!");
    }
    public void Fireball()
    {
        Debug.Log("You casted Fireball!");
        Vector3 spawnPos = transform.position + transform.forward * fireSpawnDistance;
        GameObject newfireball = Instantiate(fireball, spawnPos, transform.rotation);
        Destroy(newfireball, 5f);
    }
    public void WaterSplash()
    {
        Debug.Log("You casted Water Splash!");
        Vector3 spawnPos = transform.position + transform.forward * waterSpawnDistance;
        GameObject newWaterSplash = Instantiate(waterSplash, spawnPos, transform.rotation);
        Destroy(newWaterSplash, waterDuration);
    }

}
