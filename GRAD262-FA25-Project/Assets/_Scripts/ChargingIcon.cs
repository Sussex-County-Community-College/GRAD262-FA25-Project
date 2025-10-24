using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingIcon : MonoBehaviour
{
    GameObject player;
    Spells spells;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        spells = player.GetComponent<Spells>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + player.transform.forward * spells.lightningSpawnDistance;

        if (Input.GetKeyUp(KeyCode.UpArrow))
            Destroy(gameObject);
    }
}
