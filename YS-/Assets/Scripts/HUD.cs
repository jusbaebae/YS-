using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health } //ui타입
    public InfoType type;

    Text myText;
    Slider myslider;

    void Awake()
    {
        myText = GetComponent<Text>();
        myslider = GetComponent<Slider>();    
    }

    void LateUpdate()
    {
        switch (type) {
            case InfoType.Exp: //경험치UI
                float curExp = GameManager.inst.exp;
                float maxExp = GameManager.inst.nextExp[GameManager.inst.level];
                myslider.value = curExp / maxExp;
                break;
            case InfoType.Level: //레벨UI
                myText.text = string.Format("Lv.{0:F0}", GameManager.inst.level);
                break;
            case InfoType.Kill: //킬수UI
                myText.text = string.Format("{0:F0}", GameManager.inst.kill);
                break;
            case InfoType.Time: //시간UI
                float remainTime = GameManager.inst.maxGameTime - GameManager.inst.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}",min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.inst.health;
                float maxHealth = GameManager.inst.maxHealth;
                myslider.value = curHealth / maxHealth;
                break;
        }
    }
}
