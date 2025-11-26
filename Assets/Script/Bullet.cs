using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigid;
    public int bulletDamage;
    public GameObject hitParticlePrefab;

    private void Start()
    {
        // 총알 데미지 공식
        bulletDamage = GameManager.instance.player.atk;

        rigid = GetComponent<Rigidbody2D>();    

        rigid.AddForce(transform.right * 1000 * Time.fixedDeltaTime, ForceMode2D.Impulse);
        Destroy(gameObject, 3.0f);
    }

    public void Hit()
    {
        GameObject Particle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        Destroy(Particle, 2.0f);
    }
}
