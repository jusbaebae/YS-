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
        public int weaponmax = 0; //무기 상한
        public int gearmax = 0; //장신구 상한
        public int weaponcount; //무기 개수
        public int gearcount; //장신구 개수

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

        //기본무기 지급
        public void Select(int i)
        {
            items[i].onClick();
        }

        void Next()
        {
            // 1. 모든 아이템 비활성화
            foreach (Item item in items)
            {
                item.gameObject.SetActive(false);
            }
            // 2. 그 중에서 랜덤 4개 아이템 활성화
            // 3. 동일 아이템 제외 5레벨 달성 아이템 제외
            int[] ran = RandIntArray(4);
            for (int i = 0; i < ran.Length; i++)
            {
                Item ranItem = items[ran[i]];
                ranItem.gameObject.SetActive(true);
            }
        }
        bool CompareArray(int[] ran, int i)//중복을 체크할일이 없어서 안쓰임
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
                        count++; //상한이아닌데 만렙을찍은게있다면 그거빼기
                }
            }
            else //무기상한
            {
                count += weaponcount - weaponmax; //상한이라면 다빼기
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].level == 5 && items[j].weaponchoice == 2)
                        count++; //상한이어도 만렙을찍은게있다면 그거빼기
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
            else //장신구상한
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

            //해당무기만 넣는 코드
            if (weaponmax == 3 || gearmax == 3)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    //무기가 상한이고 선택이되었으면 그거넣기
                    if (items[j].weaponchoice == 2 && weaponmax == 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //무기는 상한이지만 장신구가 상한이아니면 넣기
                    else if (items[j].gearchoice == 1 && gearmax < 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //이미 선택된 장신구라도 상한아니라면 넣기
                    else if (items[j].gearchoice == 2 && gearmax < 3 && items[j].level != 5)
                    {
                        availableIndices.Add(j);
                    }
                    //위코드랑동일(장신구)
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
                    //힐팩(슬롯이 4미만으로가면 넣기)
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
                    break; // availableIndices 리스트가 비어 있다면 루프 종료
                }
            }

            if (length <= 3)
                for (int j = 3; j > length - 1; j--)
                    ran[j] = 4;

            return ran;
        }
    }
}
