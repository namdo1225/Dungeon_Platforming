/**
 * Description: Class to handle spawning of a prefab (usually enemies) when player is in a defined collision area.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> SpawnPoints;

    // When the player entered the collision zone, spawn a defined prefab at the location of the children objects.
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
