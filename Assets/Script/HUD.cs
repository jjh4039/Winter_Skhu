using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("# Component")]
    public CanvasGroup hpCanvas;
    public Text hpText;
    public Text playerHpText;
    public Image hpImage;
    public Text enemyNameText;
    public Slider enemyHpBar;
    public Text noticeText;
    public Text desText;
    public Text floorText;
    public Text floorSubText;
    public Text atkText;
    public Text atkDesText;
    public CanvasGroup noticeAlpha;
    public CanvasGroup floorAlpha;
    public CanvasGroup desAlpha;
    public CanvasGroup atkDesAlpha;
    public CanvasGroup alpha;
    public GameObject[] spawners;

    [Header("Data")]
    public Sprite[] hpSprites;

    public void Start()
    {
        StartCoroutine("Floor");
    }

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

        atkText.text = "Atk : " + GameManager.instance.player.atk;
        playerHpText.text = "Hp : " + GameManager.instance.player.currentHp + " / " + GameManager.instance.player.mayHp;
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
            case Enemy.EnemyName.EpicSpawner:
                enemyNameText.color = new Color(1f, 0.3f, 0.2f, 1f);
                hpText.color = new Color(1f, 0.3f, 0.2f, 1f);
                hpImage.sprite = hpSprites[2];
                enemyNameText.text = "상급 생성기 ★★";
                break;
            case Enemy.EnemyName.Boss:
                enemyNameText.color = new Color(0.4f, 0.4f, 0.4f, 1f);
                hpText.color = new Color(0.4f, 0.4f, 0.4f, 1f);
                hpImage.sprite = hpSprites[4];
                enemyNameText.text = "얼음 코어 ★★★";
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

    public IEnumerator AtkUp(int index)
    {
        switch (index)
        {
            case 0:
                atkDesText.text = "몬스터 처치 +1";
                break;
            case 1:
                atkDesText.text = "스테이지 클리어 +10";
                break;
        }

        for (int i = 0; i < 20; i++) {
            atkDesAlpha.alpha += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 20; i++)
        {
            atkDesAlpha.alpha -= 0.05f;
            yield return new WaitForSeconds(0.01f);
        }               
    }

    public IEnumerator Floor()
    {
        switch (GameManager.instance.currentIndex)
        {
            case 1:
                floorSubText.text = "얼음 연못";
                break;
            case 2:
                floorSubText.text = "얼음 성";
                break;
            default:
                break;
        }

        floorText.text = GameManager.instance.currentIndex + "층";

        for (int i = 0; i < 30; i++)
        {
            floorAlpha.alpha += 0.04f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 40; i++)
        {
            floorAlpha.alpha -= 0.03f;
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

    public IEnumerator Up(int index)
    {
        for (int i = 0; i < 30; i++) 
        {
        alpha.alpha += 0.04f;
        yield return new WaitForSeconds(0.01f);
        }

        GameManager.instance.currentIndex = index + 2;

        switch (index)
        {
            case 0:
                GameManager.instance.player.transform.position = new Vector3(-10f, 6.5f, 0f);
                Instantiate(spawners[0], new Vector3(11.79f, 6.36f, 0f), Quaternion.identity);
                break;
        }

        for (int i = 0; i < 30; i++)
        {
            alpha.alpha -= 0.04f;
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine("Floor");
    }
}
