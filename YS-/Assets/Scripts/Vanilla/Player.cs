using System.ComponentModel;
using UnityEngine;
namespace vanilla
{
    using UnityEngine.InputSystem;
    public class Player : MonoBehaviour
    {
        public Vector2 inputVec;
        Rigidbody2D rigid;
        SpriteRenderer spriter;
        public float speed = 100;
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
        }
        private void FixedUpdate()
        {
            if (!GameManager.inst.isLive)
                return;
            // 위치 이동
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
    }
}
