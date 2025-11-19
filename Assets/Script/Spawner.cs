using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int spawnerIndex;


    void Awake()
    {
        StartCoroutine(Spawn(spawnerIndex));
    }

    IEnumerator Spawn(int spawnerIndex)
    {
        switch (spawnerIndex)
        {
            // 하급 생성기 : 3초 마다 얼음 생성
            case 0:
                yield return new WaitForSeconds(3.0f);
                Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
                break;
            // 중급 생성기 : 4초 마다 얼음(60%) or 눈사람(40%) 생성
            case 1:
                yield return new WaitForSeconds(4.0f);
                int random = Random.Range(0, 10);

                if (random <= 3) Instantiate(enemyPrefabs[1], transform.position, Quaternion.identity);
                else Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
                break;
        }

        // 반복
        StartCoroutine(Spawn(spawnerIndex));
    }
}
