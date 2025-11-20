using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("# Component")]
    public CanvasGroup hpCanvas;
    public Text hpText;
    public Image hpImage;
    public Text enemyNameText;
    public Slider enemyHpBar;
    public Text noticeText;
    public Text desText;
    public Text atkText;
    public CanvasGroup noticeAlpha;
    public CanvasGroup desAlpha;

    [Header("Data")]
    public Sprite[] hpSprites;

    public void Update()
    {
        if (GameManager.instance.scanner.targetEnemy != null)
        {
            hpCanvas.alpha = 1;
            hpText.text = GameManager.instance.scanner.targetEnemy.health + " / " + GameManager.instance.scanner.targetEnemy.maxHealth;
            enemyHpBar.value = (float)GameManager.instance.scanner.targetEnemy.health / GameManager.instance.scanner.targetEnemy.maxHealth;
            EnemySet();
        }
        else
        {
            hpCanvas.alpha = 0;
        }

        atkText.text = "Player Atk : " + GameManager.instance.player.atk;
}

    public void EnemySet()
    {
        Enemy targetEnemyScirpt = GameManager.instance.scanner.targetEnemy.GetComponent<Enemy>();

        switch (targetEnemyScirpt.enemyName)
        {
            case Enemy.EnemyName.Spwaner:
                enemyNameText.color = new Color(0f, 0.25f, 0.7f, 1f);
                hpText.color = new Color(0f, 0.25f, 0.7f, 1f);
                hpImage.sprite = hpSprites[1];
                enemyNameText.text = "하급 생성기 ★";
                break;
            case Enemy.EnemyName.RareSpawner:
                enemyNameText.color = new Color(1f, 0.43f, 0.25f, 1f);
                hpText.color = new Color(1f, 0.43f, 0.25f, 1f);
                hpImage.sprite = hpSprites[2];
                enemyNameText.text = "중급 생성기 ★★";
                break;
            case Enemy.EnemyName.EpicSpawner:
                // enemyNameText.color = new Color(0f, 0.25f, 0.7f, 1f); 색 추가
                // hpText.color = new Color(0f, 0.25f, 0.7f, 1f);
                hpImage.sprite = hpSprites[3];
                enemyNameText.text = "상급 생성기 ★★★";
                break;
            case Enemy.EnemyName.IceBlock:
                enemyNameText.color = Color.black;
                hpText.color = Color.black;
                hpImage.sprite = hpSprites[0];
                enemyNameText.text = "얼음 조각";
                break;
            case Enemy.EnemyName.SnowMan:
                enemyNameText.color = Color.black;
                hpText.color = Color.black;
                hpImage.sprite = hpSprites[0];
                enemyNameText.text = "거대 눈사람";
                break;
        }
    }

    public IEnumerator DoorClose()
    {
        desText.text = "문이 굳게 잠겨 있습니다.";

        for (int i = 0; i < 20;i++) 
        {
            desAlpha.alpha += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < 40; i++)
        {
            desAlpha.alpha -= 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator Key()
    {
        noticeText.text = (GameManager.instance.clearIndex + 1)  + "층으로 이동할 수 있는\n<color=#50bcdf>문</color>이 개방되었습니다.";

        for (int i = 0 ; i < 30 ; i++)
        {
            noticeAlpha.alpha += 0.04f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 40; i++)
        {
            noticeAlpha.alpha -= 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
