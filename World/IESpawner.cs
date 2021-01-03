using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Core
{
public class IESpawner : MonoBehaviour
{
    public GameObject enemy;
    public float respawnCheckTime = 20;

    void Awake()
    {
        Spawning();
        StartCoroutine(CheckIfEnemyIsAlive());
    }


    private void Spawning()
    {
        Instantiate(enemy, transform.position, Quaternion.identity, this.gameObject.transform);
    }

    IEnumerator CheckIfEnemyIsAlive()
    {
        while(true)
        {
        if(transform.childCount == 1)
        {
        }
        else if (transform.childCount == 0)
        {
            Spawning();
        }
        yield return new WaitForSeconds(respawnCheckTime);   
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        position[1] = transform.position[1] + 5f;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawIcon(position, "T_5_sword_.png", true);
        Gizmos.DrawLine(transform.position, position);
    }
}
}