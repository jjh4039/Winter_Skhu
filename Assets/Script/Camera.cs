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
        if (GameManager.instance.currentIndex != 3) transform.position = new Vector3(Mathf.Clamp(target.x, -7.5f, 4f), Yset(), target.z);
        else transform.position = new Vector3(-6f, Yset(), target.z);  
    }

    public float Yset()
    {
        switch (GameManager.instance.currentIndex) {
            case 1:
                return -1.1f;
            case 2:
                return 9.45f;
            case 3:
                return 19.55f;
            default:
                return 0;
        }
    }
}
