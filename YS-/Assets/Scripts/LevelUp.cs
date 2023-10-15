using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class LevelUp : MonoBehaviour
    {
        RectTransform rect;
        Item[] items;
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
            // 1. ��� ������ ��Ȱ��ȭ
            foreach (Item item in items)
            {
                item.gameObject.SetActive(false);
            }
            // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
            // 3. ���� ������ ���� 5���� �޼� ������ ����
            int[] ran = RandIntArray(3);
            for (int i = 0; i < ran.Length; i++)
            {
                Item ranItem = items[ran[i]];
                ranItem.gameObject.SetActive(true);
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
            for (int j = 0; j < items.Length; j++)
            {
                if (items[j].level == 5)
                    count++;
            }
            int length = items.Length - count > index ? index : items.Length - count;
            int[] ran = new int[index];
            int i = 0;
            while (true)
            {
                ran[i] = Random.Range(0, items.Length);
                if (items[ran[i]].level != 5 && CompareArray(ran, i))
                    i++;
                if (i == length)
                    break;
            }
            if (length <= 2)
                for (int j = 2; j > length - 1; j--)
                    ran[j] = 4;

            return ran;
        }
    }
}
