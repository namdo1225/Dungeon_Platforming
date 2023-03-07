using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> SpawnPoints;

    void OnTriggerEnter(Collider col)
    {

        GameObject gameObject = col.gameObject;
        if (gameObject.tag == "Player"){
            foreach (GameObject point in SpawnPoints)
            {
                Instantiate(enemyPrefab,point.transform.position,point.transform.rotation);
            }

            Destroy(this.gameObject, 5f);
        }
    }
}
