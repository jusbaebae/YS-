using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class LevelUp : MonoBehaviour
    {
        RectTransform rect;
        Item[] items;
        int health;
        int length;
        bool isWeaponMax;
        bool isGearMax;
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            items = GetComponentsInChildren<Item>(true);
        }
        public void Show()
        {
            Next();
            rect.localScale = Vector3.one;
            GameManager.inst.Stop();
        }
        public void Hide()
        {
            rect.localScale = Vector3.zero;
            GameManager.inst.Resume();
        }

        //�⺻���� ����
        public void Select(int i)
        {
            items[i].onClick();
        }

        void Next()
        {
            int i = 0;
            // 1. ��� ������ ��Ȱ��ȭ �� ���� ��ġ Ȯ��
            foreach (Item item in items)
            {
                Debug.Log(item);
                item.gameObject.SetActive(false);
                if (item.data.itemType == ItemData.ItemType.Heal)
                    health = i;
                i++;
            }
            // 2. �� �߿��� ���� n�� ������ Ȱ��ȭ
            // 3. ���� ������ ���� 5���� �޼� ������ ����
            // 4. ���� ī�װ��� 6���� �Ǿ��ٸ� ��� ����
            if (GameManager.inst.gearCount == 6)
                isGearMax = true;
            if (GameManager.inst.weaponCount == 6)
                isWeaponMax = true;

            int[] ran = RandIntArray(4);
            for (i = 0; i < ran.Length; i++)
            {
                items[ran[i]].gameObject.SetActive(true);
            }

        }
        bool CompareArray(int[] ran, int i)
        {
            for (int j = 0; j < i; j++)
            {
                if (ran[j] == ran[i])
                    return false;
            }
            return true;
        }
        int[] RandIntArray(int index)
        {
            int count = 0;
            // �������� ������ 5�� ������ �������� ī�װ��� ���� �������� �ƴ��� Ȯ���ϱ�
            for (int j = 0; j < items.Length; j++)
                if (items[j].level == 5 || (items[j].data.itemCategory == ItemData.ItemCategory.Weapon && isWeaponMax && items[j].level == 0) || (items[j].data.itemCategory == ItemData.ItemCategory.Brooch && isGearMax && items[j].level == 0))
                    count++;

            // ������ �� �ִ� ���� �������� ������ ���ϰ� �� ������ ���� �����ؾ��ϴ� �迭���� ���� ª���� Ȯ��
            length = items.Length - count > index ? index : items.Length - count;
            int[] ran = new int[index];
            int i = 0;
            while (true)
            {
                ran[i] = Random.Range(0, items.Length);
                if (items[ran[i]].level != 5 && CompareArray(ran, i) && ran[i] != health && !(isWeaponMax == true && items[ran[i]].data.itemCategory == ItemData.ItemCategory.Weapon && items[ran[i]].level == 0) && !(isGearMax == true && items[ran[i]].data.itemCategory == ItemData.ItemCategory.Brooch && items[ran[i]].level == 0))
                    i++;
                if (i == length || (i == length && items.Length - count < index))
                {
                    for (int j = i; j < index; j++)
                        ran[j] = health;
                    break;
                }
            }
            return ran;
        }
    }
}