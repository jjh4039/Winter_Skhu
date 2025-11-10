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

    [Header("Player Info")]
    public int atk;

    void Update()
    {
        moveLR = Input.GetAxis("Horizontal");
        transform.Translate(moveLR * Time.deltaTime * 5.0f, 0, 0, Space.World);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, transform.position + new Vector3(0f, -0.35f, 0), transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(new Vector2(0, 6.0f), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
            if(rigid.velocity.y != 0)
            {
                anim.SetBool("isJump", true);
            }
        }

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
}
