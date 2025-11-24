using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject interactionGuide;
    public SpriteRenderer spriteRenderer;
    public Sprite[] doorSprites;
    public int doorIndex;
    public bool isInteraction;

    public void Start()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (GameManager.instance.clearIndex >= doorIndex + 1) // ¹® ¿­¸²
        {
            spriteRenderer.sprite = doorSprites[1];

            if (Input.GetKeyDown(KeyCode.E) && isInteraction == true)
            {
                  isInteraction = false;
                  GameManager.instance.hud.StartCoroutine("Up", doorIndex);
            }
        }
        else // ¹® ´ÝÈû
        {
            spriteRenderer.sprite = doorSprites[0]; 

            if (Input.GetKeyDown(KeyCode.E) && isInteraction == true)
            {
                GameManager.instance.hud.StopCoroutine("DoorClose");
                GameManager.instance.hud.StartCoroutine("DoorClose");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionGuide.SetActive(true);
            isInteraction = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionGuide.SetActive(false);
            isInteraction = false;
        }
    }
}
