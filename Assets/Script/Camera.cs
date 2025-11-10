using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    Vector3 target;

    void Update()
    {
        target = new Vector3(player.position.x, player.position.y, player.position.z -10);

        // 카메라 세팅
        transform.position = new Vector3(Mathf.Clamp(target.x, -7.5f, 4f), Mathf.Clamp(target.y, -1.1f, 20f), target.z);
    }
}
