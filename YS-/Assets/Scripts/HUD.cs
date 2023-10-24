using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
namespace vanilla
{
    public class HUD : MonoBehaviour
    {
        public enum InfoType { Exp, Level, Kill, Time, Health }
        public InfoType type;

        Text tmp;
        Slider mySlider;

        private void Awake()
        {
            tmp = GetComponent<Text>();
            mySlider = GetComponent<Slider>();
        }

        private void LateUpdate()
        {
            switch (type)
            {
                case InfoType.Exp:
                    float curExp = GameManager.inst.exp;
                    float maxExp = GameManager.inst.nextExp[Mathf.Min(GameManager.inst.level, GameManager.inst.nextExp.Length - 1)];
                    mySlider.value = curExp / maxExp;
                    break;
                case InfoType.Level:
                    tmp.text = string.Format("Lv.{0:F0}", GameManager.inst.level);
                    break;
                case InfoType.Kill:
                    tmp.text = string.Format("x {0:F0}", GameManager.inst.kill);
                    break;
                case InfoType.Time:
                    float remainT = GameManager.inst.maxGameTime - GameManager.inst.gameTime;
                    tmp.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(remainT / 60f), Mathf.FloorToInt(remainT % 60f));
                    break;
                case InfoType.Health:
                    float curHealth = GameManager.inst.health;
                    float maxHealth = GameManager.inst.GetMaxHP();
                    mySlider.value = curHealth / maxHealth;
                    break;
                default:
                    break;
            }
        }
    }
}

