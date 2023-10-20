using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class LevelUp : MonoBehaviour
    {
        RectTransform rect;
        Item[] items;
        public ItemData data;
        public int weaponmax = 0; //���� ����
        public int gearmax = 0; //��ű� ����
        public int weaponcount; //���� ����
        public int gearcount; //��ű� ����

        public static bool state;

        private void Awake()
        {
            state = false;
            rect = GetComponent<RectTransform>();
  
        }
        private void Start()
        {
            items = GetComponentsInChildren<Item>(true);
        }
        public void Show()
        {
            state = true;
            Next();
            rect.localScale = Vector3.one;
            GameManager.inst.Stop();
        }
        public void Hide()
        {
            state = false;
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
            // 2. �� �߿��� ���� 4�� ������ Ȱ��ȭ
            // 3. ���� ������ ���� 5���� �޼� ������ ����
            int[] ran = RandIntArray(4);
            for (int i = 0; i < ran.Length; i++)
            {
                Item ranItem = items[ran[i]];
                ranItem.gameObject.SetActive(true);
            }
        }
        bool CompareArray(int[] ran, int i)//�ߺ��� üũ������ ��� �Ⱦ���
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
            if (weaponmax != 3)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].level == 5 && items[j].weaponchoice == 2)
                        count++; //�����̾ƴѵ� �������������ִٸ� �װŻ���
                }
            }
            else //�������
            {
                count += weaponcount - weaponmax; //�����̶�� �ٻ���
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].level == 5 && items[j].weaponchoice == 2)
                        count++; //�����̾ �������������ִٸ� �װŻ���
                }
            }

            if (gearmax != 3)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].level == 5 && items[j].gearchoice == 2)
                        count++;
                }
            }
            else //��ű�����
            {
                count += gearcount - gearmax;
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].level == 5 && items[j].gearchoice == 2)
                        count++;
                }
            }
            int length = items.Length - count > index ? index : items.Length - count;
            int[] ran = new int[index];
            int i = 0;
            List<int> availableIndices = new List<int>();

            //�ش繫�⸸ �ִ� �ڵ�
            if (weaponmax == 3 || gearmax == 3)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    //���Ⱑ �����̰� �����̵Ǿ����� �װųֱ�
                    if (items[j].weaponchoice == 2 && weaponmax == 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //����� ���������� ��ű��� �����̾ƴϸ� �ֱ�
                    else if (items[j].gearchoice == 1 && gearmax < 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //�̹� ���õ� ��ű��� ���Ѿƴ϶�� �ֱ�
                    else if (items[j].gearchoice == 2 && gearmax < 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //���ڵ������(��ű�)
                    if (items[j].gearchoice == 2 && gearmax == 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    else if (items[j].weaponchoice == 1 && weaponmax < 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    else if (items[j].weaponchoice == 2 && weaponmax < 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //����(������ 4�̸����ΰ��� �ֱ�)
                    if (items[j].weaponchoice == 0 && items[j].gearchoice == 0 && count >= 6) 
                    {
                        availableIndices.Add(j);
                    }
                }
            }
            else
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].weaponchoice == 0 && items[j].gearchoice == 0 && count != 6) 
                    {}
                    else
                    {
                        if(items[j].level != 5)
                        {
                            availableIndices.Add(j);
                        }
                    }
                }
            }

            while (true)
            {
                if (availableIndices.Count > 0)
                {
                    int randomIndex = Random.Range(0, availableIndices.Count);
                    ran[i] = availableIndices[randomIndex];
                    availableIndices.RemoveAt(randomIndex);
                    i++;
                    if (i == length)
                        break;
                }
                else
                {
                    break; // availableIndices ����Ʈ�� ��� �ִٸ� ���� ����
                }
            }

            if (length <= 3)
                for (int j = 3; j > length - 1; j--)
                    ran[j] = 4;

            return ran;
        }
    }
}
