using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public string[] endingStrings;
    public Text[] endingTexts;
    public CanvasGroup alpha;

    private void Start()
    {
        StartCoroutine(EndingStart());
    }

    IEnumerator EndingStart()
    {
        alpha.alpha = 1f;
        for (int i = 0; i < 40; i++)
        {
            alpha.alpha -= 0.025f;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);

        /*
        for (int i = 0; i < endingTexts.Length; i++)
        {
            endingTexts[i].text = endingStrings[i];
            yield return new WaitForSeconds(2f);
        }
        */
    }
}
