using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyName { Spwaner, IceBlock }
    public Transform player;
    public Animator anim;
    
    [Header ("Enemy Info")]
    [SerializeField] EnemyName enemyName;
    public int maxHealth;
    public int health;
    public bool isMove = true;
    public bool isLive = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameManager.instance.player.transform;

        Init(); // 몬스터 정보 초기화 ( 체력 )
        StartCoroutine(Moving()); // 특수한 반복 동작 실행
    }

    void Update()
    {
        switch(enemyName)
        {
            case EnemyName.IceBlock:
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
                health -= expectDamage;
                StartCoroutine(KnockBack());
            }
            else // 사망  
            {
                health = 0;
                isLive = false;
                gameObject.tag = "Untagged";
                anim.SetBool("isDie", true);
                Destroy(gameObject, 1.0f);
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
            case EnemyName.IceBlock:
                maxHealth = 300;
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
                // 플레이어 추적
                if (player.position.x < transform.position.x)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else
                    transform.rotation = Quaternion.Euler(0, 180, 0);

                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Moving());
                break;
        }
    }
}
