using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    void Awake()
    {
        StartCoroutine(Spawn(0));
    }

    IEnumerator Spawn(int enemyID)
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(enemyPrefabs[enemyID], transform.position, Quaternion.identity);

        StartCoroutine(Spawn(0));
    }
}
