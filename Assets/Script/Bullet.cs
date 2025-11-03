using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.AddForce(transform.right * 1000 * Time.fixedDeltaTime, ForceMode2D.Impulse);
        Destroy(gameObject, 3.0f);
    }
}
