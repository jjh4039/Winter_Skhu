using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveLR = 0;
    public Rigidbody2D rigid;
    public Animator anim;

    void Update()
    {
        moveLR = Input.GetAxis("Horizontal");
        transform.Translate(moveLR * Time.deltaTime * 5.0f, 0, 0, Space.World);

        if (Input.GetKeyDown(KeyCode.W))
        {
            rigid.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
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
