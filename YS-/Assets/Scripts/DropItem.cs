using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace vanilla
{
    public class DropItem : MonoBehaviour
    {
        public DropSets[] datas;
        SpriteRenderer sprite;
        public int itemId;
        string itemName;

        [SerializeField] public ItemType type;
        public float moveSpeed; //속도
        public float magnetDistance; //범위
        bool isFoundP;

        Transform player;

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            player = GameManager.inst.player.transform;
        }
        private void OnEnable()
        {
            isFoundP = false;
            foreach (DropSets data in datas)
            {
                float randn = Random.value;
                if (randn <= (data.ratio * 0.01) * GameManager.inst.player.luck)
                {
                    type = data.itemType;
                    itemId = data.itemId;
                    sprite.sprite = data.itemSprite;
                    itemName = data.itemName;
                    break;
                }
            }
            switch (type)
            {
                case ItemType.Item:
                    moveSpeed = 8f;
                    magnetDistance = 1f;
                    break;
                case ItemType.Exp:
                    moveSpeed = 8f;
                    magnetDistance = 2f;
                    break;
            }
        }
        void Update()
        {
            //거리계산
            float distance = Vector2.Distance(transform.position, player.position);

            //거리가 자석범위 보다 낮으면 끌리게
            if (distance <= magnetDistance)
                isFoundP = true;

            if (isFoundP)
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        public void ApplyItem()
        {
            switch (itemId)
            {
                case 0:
                    GameManager.inst.health += GameManager.inst.maxHealth * 0.1f;
                    if (GameManager.inst.health > GameManager.inst.maxHealth)
                        GameManager.inst.health = GameManager.inst.maxHealth;
                    break;
                case 1:
                    DropItem[] dropItems = GameManager.inst.pool.gameObject.GetComponentsInChildren<DropItem>();
                    foreach (DropItem dropitem in dropItems)
                    {
                        if (dropitem.type == ItemType.Exp)
                        {
                            dropitem.moveSpeed = 20f;
                            dropitem.magnetDistance += dropitem.magnetDistance * 10000;
                        }
                    }
                    break;
                case 2:
                    GameManager.inst.ClearField(true);
                    break;
                case 3:
                    GameManager.inst.player.luck += 0.1f;
                    break;
                case 4:
                    GameManager.inst.GetExp(5);
                    break;
                case 5:
                    GameManager.inst.GetExp(3);
                    break;
                case 6:
                    GameManager.inst.GetExp(1);
                    break;
            }
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                ApplyItem();
                gameObject.SetActive(false);
                if (itemId == 0 || itemId == 3 || itemId == 4 || itemId == 5 || itemId == 6)
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Drop);
            }
        }
    }

    [System.Serializable]
    public class DropSets
    {
        [Header("# Main Info")]
        public ItemType itemType;
        public int itemId;
        public Sprite itemSprite;
        public string itemName;
        [TextArea]
        public string itemDesc;
        public float ratio;
    }
    public enum ItemType
    {
        Exp,
        Item
    }
}

