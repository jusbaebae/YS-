using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace vanilla
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager inst;

        [Header("# Game Control")]
        public PoolManager pool;
        public Player player;
        public bool isLive;
        public bool noExp;
        public int weaponCount;
        public int gearCount;
        public Map map;

        [Header("# Player Info")]
        public float health = 0;
        public float maxHealth = 100;
        public float originHealth = 100;
        public int level;
        public int kill;
        public int exp = 0;
        public int[] nextExp = new int[50];

        [Header("# Game Object")]
        public float gameTime = 0f;
        public float maxGameTime = 2 * 10f;
        public LevelUp uiLevelUp;
        public Result uiResult;
        public GameObject enemyCleaner;

        private void Awake()
        {
            inst = this;
            for (int i = 0; i < 50; i++)
                nextExp[i] = 10 + (30 * i);
        }
        public void GameStart()
        {
            health = maxHealth;
            Resume();
            uiLevelUp.Select(0);  

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
            AudioManager.instance.PlayBgm(true);
        }
        public void GameOver()
        {
            StartCoroutine(GameOverRoutine());
        }
        public void ClearField(bool luck)
        {
            StartCoroutine(FieldClear(luck));
        }
        IEnumerator FieldClear(bool luck)
        {
            noExp = luck;
            enemyCleaner.SetActive(true);
            yield return new WaitForSeconds(2f);
            enemyCleaner.SetActive(false);
            noExp = false;
        }
        IEnumerator GameOverRoutine()
        {
            isLive = false;
            yield return new WaitForSeconds(0.5f);
            uiResult.gameObject.SetActive(true);
            uiResult.Lose();
            Stop();

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
            AudioManager.instance.PlayBgm(false);
        }
        public void GameVictory()
        {
            StartCoroutine(GameVictoryRoutine());
        }
        IEnumerator GameVictoryRoutine()
        {
            isLive = false;
            enemyCleaner.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            uiResult.gameObject.SetActive(true);
            uiResult.Win();
            Stop();

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
            AudioManager.instance.PlayBgm(false); ;
        }
        public void GameRetry()
        {
            SceneManager.LoadScene(1);
        }
        public void GetExp(int n)
        {
            if (!isLive)
                return;
            exp += n;
            if (nextExp[Mathf.Min(level, nextExp.Length - 1)] <= exp)
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
                GameVictory();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                uiLevelUp.Show();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameRetry();
            }
        }

        public void Stop()
        {
            isLive = false;
            Time.timeScale = 0;
        }
        public void Resume()
        {
            isLive = true;
            Time.timeScale = 1;
        }
        void LateUpdate()
        {
            //Observer.instance.kill = kill;
        }
    }
}
