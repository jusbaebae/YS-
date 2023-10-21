using System.ComponentModel;
using UnityEngine;
namespace vanilla
{
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Processors;

    public class Player : MonoBehaviour
    {
        public Vector2 inputVec;
        Rigidbody2D rigid;
        SpriteRenderer spriter;
        public float speed = 5;
        public float luck;
        public float attack;
        public float defend;
        public float baseDefend;
        public float baseAttack;
        public float critical;
        public float expBonus;
        Animator ani;

        public Hands hands;
        public Scanner scanner;
        public CapsuleCollider2D col;
        public BoxCollider2D col2;
        // Start is called before the first frame update
        void Awake()
        {
            col = GetComponent<CapsuleCollider2D>();  
            col2 = GetComponent<BoxCollider2D>();
            rigid = GetComponent<Rigidbody2D>();
            spriter = GetComponent<SpriteRenderer>();
            ani = GetComponent<Animator>();
            scanner = GetComponent<Scanner>();
            hands = GetComponentInChildren<Hands>(true);

            luck = DataManager.instance.currentCharData.luck;
            baseAttack = DataManager.instance.currentCharData.attack;
            baseDefend = DataManager.instance.currentCharData.defense;
            attack = baseAttack;
            defend = baseDefend;
            critical = 0f;
            expBonus = 1f;

            ani.runtimeAnimatorController = new AnimatorOverrideController(DataManager.instance.currentCharData.play_anim);
        }
        private void FixedUpdate()
        {
            if (!GameManager.inst.isLive)
                return;
            // ��ġ �̵�
            Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);

        }
        void OnMove(InputValue value)
        {
            inputVec = value.Get<Vector2>();
        }

        void LateUpdate()
        {
            if (!GameManager.inst.isLive)
                return;
            ani.SetFloat("Speed", inputVec.magnitude);
            if (inputVec.x != 0)
            {
                spriter.flipX = inputVec.x < 0;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!GameManager.inst.isLive)
                return;

            GameManager.inst.health -= Time.deltaTime * 10f;
            if(GameManager.inst.health < 0)
            {
                for (int i=2; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(false);
                ani.SetTrigger("Dead");
                GameManager.inst.GameOver();
            }
        }
    }
}
