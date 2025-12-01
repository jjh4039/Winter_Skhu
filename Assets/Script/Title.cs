using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public CanvasGroup alpha;
    public CanvasGroup startTextAlpha;
    public SpriteRenderer[] titleSprites;
    public bool isStart = false;

    private void Start()
    {
        StartCoroutine(TitleStart());
    }

    public void Update()
    {
        if (isStart && Input.anyKeyDown)
        {
            isStart = false;
        }
    }

    IEnumerator TitleStart()
    {
        alpha.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 40; i++)
        {
            alpha.alpha -= 0.025f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 60; i++)
        {
            if (i < 40)
            {
                titleSprites[0].color += new Color(0, 0, 0, 0.025f);
            }
            if (i > 20)
            {
                titleSprites[1].color += new Color(0, 0, 0, 0.025f);
            }
            yield return new WaitForSeconds(0.025f);
        }
       
        yield return new WaitForSeconds(0.5f);
        isStart = true;

        while (isStart)
        {
            startTextAlpha.alpha = 1f;
            for (int i = 0; i < 50; i++)
            {
                if (!isStart) break;
                yield return new WaitForSeconds(0.01f);
            }

            startTextAlpha.alpha = 0f;
            for (int i = 0; i < 50; i++)
            {
                if (!isStart) break;
                yield return new WaitForSeconds(0.01f);
            }
        }

        startTextAlpha.alpha = 0f;

        for (int i = 0; i < 40; i++)
        {
            alpha.alpha += 0.025f;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
