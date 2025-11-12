using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public CanvasGroup hpCanvas;
    public Text hpText;
    public Text enemyNameText;
    public Slider enemyHpBar;

    public void Update()
    {
        if (GameManager.instance.scanner.targetEnemy != null)
        {
            hpCanvas.alpha = 1;
            hpText.text = GameManager.instance.scanner.targetEnemy.health + " / " + GameManager.instance.scanner.targetEnemy.maxHealth;
            enemyHpBar.value = (float)GameManager.instance.scanner.targetEnemy.health / GameManager.instance.scanner.targetEnemy.maxHealth;
        }
        else
        {
            hpCanvas.alpha = 0;
        }
    }
}
