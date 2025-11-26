using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Rigidbody2D rigid;
    public GameObject bulletObject;
    public Animator anim;

    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        StartCoroutine("BossMove");
    }

    IEnumerator BossMove()
    {

        // 패턴 값 생성
        int patternIndex = UnityEngine.Random.Range(0, 3);
        Debug.Log(patternIndex);
            
        switch (patternIndex)
        {
            case 0: // 돌진 (노란색)
                anim.SetBool("Pattern 0", true);
                yield return new WaitForSeconds(1.2f);

                for (int i = 0; i < 110; i++)
                {
                    transform.Translate(Vector2.left * 0.1f);
                    yield return null;
                }
                anim.SetBool("Pattern 0", false);
                break;
            case 1: // 하늘 투사체 생성
                anim.SetBool("Pattern 1", true);
                yield return new WaitForSeconds(1.2f);
                for (int i = 0;i <= 6; i++)
                {
                    Instantiate(bulletObject, new Vector2(-11.82f + (i * 2f), 27.28f), Quaternion.Euler(0f, 0f, -90f));
                    yield return new WaitForSeconds(0.4f);
                }
                anim.SetBool("Pattern 1", false);
                break;
            case 2: // 점프 유도 패턴 (빨간색)
                anim.SetBool("Pattern 2", true);
                yield return new WaitForSeconds(1.5f);

                // 위치 감지 후 보스 방향에서 투사체 생성
                if (GameManager.instance.player.transform.position.x < transform.position.x)
                {
                    Instantiate(bulletObject, new Vector2(6.3f, 16.5f), Quaternion.Euler(0f, 0f, 180f));
                }
                else
                {
                    Instantiate(bulletObject, new Vector2(-18f, 16.5f), Quaternion.Euler(0f, 0f, 0f));
                }
                yield return new WaitForSeconds(1f);
                anim.SetBool("Pattern 2", false);
                break;
        }

        // 패턴 진행 후 대기 상태
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 20; i++)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.01f);
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < 20; i++)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
                yield return new WaitForSeconds(0.02f);
            }

            // 플레이어 추적
            if (GameManager.instance.player.transform.position.x < transform.position.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        StartCoroutine("BossMove");
    }
}
