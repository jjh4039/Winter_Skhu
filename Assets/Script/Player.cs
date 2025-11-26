using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveLR = 0;

    public Rigidbody2D rigid;
    public Animator anim;
    public GameObject bullet;
    public SpriteRenderer spriteRenderer;

    [Header("Player Info")]
    public int atk;
    public int mayHp;
    public int currentHp;
    public bool isHit;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        currentHp = mayHp;
        isHit = false;
    }

    void Update()
    {
        moveLR = Input.GetAxis("Horizontal");
        transform.Translate(moveLR * Time.deltaTime * 5.0f, 0, 0, Space.World);

        // 공격 (총알 생성)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, transform.position + new Vector3(0f, -0.35f, 0), transform.rotation);
        }

        // 점프 (이중 점프 불가)
        if (Input.GetKeyDown(KeyCode.W) && rigid.velocity.y == 0 || Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y == 0)
        {
            rigid.AddForce(new Vector2(0, 6.0f), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
            if (rigid.velocity.y != 0)
            {
                anim.SetBool("isJump", true);
            }
        }

        // 이동
        if (moveLR != 0)
        {
            anim.SetBool("isRun", true);
            if (moveLR > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else anim.SetBool("isRun", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isHit == false)
        {
            StartCoroutine("Hit", 5);
        }

        if (collision.CompareTag("EnemyBullet") && isHit == false)
        {
            StartCoroutine("Hit", 3);
        }
    }

    IEnumerator Hit(int damage)
    {
        isHit = true;
        currentHp -= damage;
        Debug.Log(currentHp + " / " + mayHp);

        for (int i = 0;i < 2; i++) {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }

        isHit = false;
    }
}


