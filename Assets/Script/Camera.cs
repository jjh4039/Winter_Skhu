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

        transform.position = new Vector3(Mathf.Clamp(target.x, -5, 5), target.y, target.z);
    }
}
