using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] doors;

    private CameraController camController;
    private int enemyCnt;
    private bool completed = false;

    private void Start()
    {
        camController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        enemyCnt = enemies.Length;
        Time.timeScale = 1f;
    }
    
    private void Update()
    {
        if (enemyCnt == 0)
        {
            StartCoroutine(DestroyDoors());
            completed = true;
        }
    }

    public IEnumerator StartEncounter()
    {
        yield return new WaitForSeconds(camController.duration);

        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(true);
        }
    }

    public void DecreaseEnemies()
    {
        enemyCnt--;
        Debug.Log("Enemy Killed!");
    }

    private IEnumerator DestroyDoors()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < doors.Length; i++)
        {
            Destroy(doors[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!completed && other.CompareTag("Player"))
        {
            StartCoroutine(StartEncounter());
        }
    }
}
