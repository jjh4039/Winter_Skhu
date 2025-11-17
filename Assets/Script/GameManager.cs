using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public Player player;
    public Scanner scanner;
    public HUD hud;

    public int clearIndex = 0;

    private void Awake()
    {
        instance = this;
    }
}
