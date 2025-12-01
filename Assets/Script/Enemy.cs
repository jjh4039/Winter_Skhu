using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyName { Spwaner, IceBlock, SnowMan, EpicSpawner, Boss}
    public Animator anim;
    public Collider2D colider;
    public GameObject hitGameObject;
    
    [Header ("Enemy Info")]
    [SerializeField] public EnemyName enemyName;
    public int maxHealth;
    public int health;
    public bool isMove = true;
    public bool isLive = true;
    public bool isSpwaner;

    private void Start()
    {   
        // 스포너가 아니면 애니메이션
        if (!isSpwaner) anim = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();

        Init(); // 몬스터 정보 초기화 ( 체력 )
        StartCoroutine(Moving()); // 특수한 반복 동작 실행
    }

    void Update()
    {
        switch(enemyName)
        {
            case EnemyName.IceBlock:
            case EnemyName.SnowMan:
                if (isMove && isLive) // 피격 당하지 않았고, 몬스터가 살아있다면
                    transform.Translate(Vector2.left * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && isLive)
        {
            // 접촉 총알 처리
            GameObject bullet = collision.gameObject;
            Bullet script = bullet.GetComponent<Bullet>();
            int expectDamage = script.bulletDamage;
            script.Hit();

            Destroy(bullet);

            // 피격 처리
            if (health > expectDamage) // 일반 피격
            {
                if (!script.isHit)
                {
                    health -= expectDamage;
                    script.isHit = true;
                }
                else return;

                // 스포너가 아니면 넉백 애니메이션 및 기능 발동
                if (!isSpwaner)
                {
                    StopCoroutine(KnockBack());
                    StartCoroutine(KnockBack());
                }
            }
            else // 사망  
            {
                if (script.isHit) return;

                health = 0;
                isLive = false;
                gameObject.tag = "Untagged";
                hitGameObject.tag = "Untagged";

                // 스포너가 아니면 사망 애니메이션
                if (!isSpwaner)
                {
                    anim.SetBool("isDie", true);
                    Destroy(gameObject, 1.0f);

                    GameManager.instance.player.atk++;
                    GameManager.instance.hud.StartCoroutine("AtkUp", 0);
                }

                // 스포너 일 시 즉시 삭제 및 열쇠 상호작용
                else
                {
                    GameManager.instance.clearIndex++;
                    GameManager.instance.player.atk += 10;
                    GameManager.instance.hud.StartCoroutine("AtkUp", 1);
                    GameManager.instance.hud.StartCoroutine("Key");
                    Destroy(gameObject);
                }
            }
        }
    }

    void Init()
    {
        switch (enemyName)
        {
            case EnemyName.Spwaner:
                maxHealth = 1000;
                break;
            case EnemyName.EpicSpawner:
                maxHealth = 2000;
                break;
            case EnemyName.IceBlock:
                maxHealth = 200;
                break;
            case EnemyName.SnowMan:
                maxHealth = 600;
                break;
            case EnemyName.Boss:
                maxHealth = 3000;
                break;
        }

        health = maxHealth;
    }

    IEnumerator KnockBack()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 8);

        for (int i = 20; i > 0; i--)
        {
            isMove = false;
            anim.SetBool("isHit", true);
            yield return new WaitForSeconds(0.01f);
        }

        isMove = true;
        anim.SetBool("isHit", false);
    }

    IEnumerator Moving()
    {
        switch (enemyName)
        {
            case EnemyName.IceBlock:
            case EnemyName.SnowMan:
                // 플레이어 추적
                if (GameManager.instance.player.transform.position.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Moving());
                break;
            case EnemyName.Boss:
                break;
        }
    }
}
