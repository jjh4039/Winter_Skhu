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
    public GameObject typingSound;

    private void Start()
    {
        Time.timeScale = 1f;

        endingStrings = new string[]
        {
            "A WINTER DAY",
            "어느 겨울날",
            "프로젝트 : [게임제작] 기말 과제",
            "제작기간 : 25.10.27 ~ 25.12.05",
            "제작도구 : Unity, Asprite, Gemini",
            "플레이 해주셔서 감사합니다.",
            "Thanks for Playing."
        };

        for (int i = 0; i < endingTexts.Length; i++)
        {
            endingTexts[i].text = "";
        }
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
        yield return new WaitForSeconds(3f);

        float d = 0.1f; // 글자 당 딜레이

        for (int i = 0; i < endingStrings.Length; i++)
        {
            GameObject Sound = Instantiate(typingSound, transform.position, Quaternion.identity);
            for (int j = 0; j < endingStrings[i].Length; j++)
            {
                endingTexts[i].text += endingStrings[i][j];
                yield return new WaitForSeconds(d);
            }
            Destroy(Sound);

            yield return new WaitForSeconds(1f); // 문장 당 딜레이
        }
    }

}
