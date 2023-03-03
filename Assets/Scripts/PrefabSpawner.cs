using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> SpawnPoints;

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<IsPlayer>()){
            foreach (GameObject point in SpawnPoints)
            {
                Instantiate(enemyPrefab,point.transform.position,point.transform.rotation);
            }
        }
    }
}
