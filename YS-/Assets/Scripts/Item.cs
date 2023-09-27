using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //아이템관리에 필요한 변수들 선언
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        //버튼클릭시 이벤트생성
        switch(data.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0) {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                } else {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                } else {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                break;
            case ItemData.ItemType.Heal:
                GameManager.inst.health = GameManager.inst.maxHealth;
                break;
        }

        level++;

        //최대레벨이되면 버튼 비활성화
        if(level == data.damages.Length) {
            GetComponent<Button>().interactable = false;
        }
    }
}
