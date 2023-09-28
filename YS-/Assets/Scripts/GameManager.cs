using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [Header("# Game Control")]
    public PoolManager pool;
    public Player player;
    public Map map;
    public bool isLive;

    [Header("# Player Info")]
    public int health = 0;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp = 0;
    public int[] nextExp = new int[50];

    [Header("# Game Object")]
    public float gameTime = 0f;
    public float maxGameTime = 2 * 10f;
    public LevelUp uiLevelUp;

    private void Awake()
    {
        inst = this;
        for (int i = 0; i < 50; i++)
            nextExp[i] = 10 + (30 * i);
    }
    private void Start()
    {
        health = maxHealth;

        // 임시 스크립트(1번캐릭터용)
        uiLevelUp.Select(0);
    }
    public void GetExp()
    {
        exp++;

        if (nextExp[Mathf.Min(level, nextExp.Length - 1)] == exp)
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }
    void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            uiLevelUp.Show();
        }
    }

    public void Stop() // 레벨업시 시간정지물(스또뿌)
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume() //레벨업이 끝나면 시간정지해제
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
