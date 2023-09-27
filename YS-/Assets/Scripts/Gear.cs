using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    //장비의 타입과 수치설정
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //기본
        name = "Gear " + data.itemId;
        transform.parent = GameManager.inst.player.transform; //부모설정
        transform.localPosition = Vector3.zero; //위치초기화

        //프로퍼티
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }
    
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons) {
            switch(weapon.id) {
                //무기타입에 따라 무기속도 올리기
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }
    void SpeedUp()
    {
        float speed = 3;
        GameManager.inst.player.speed = speed + speed * rate;
    }
}
