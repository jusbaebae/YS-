using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace vanilla
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Transform playerTransform;
        [SerializeField]
        Vector3 cameraPosition;

        [SerializeField]
        Vector2 center;

        [SerializeField]
        float cameraMoveSpeed;
        float height;
        float width;
        //카메라 흔들기
        public float ShakeAmount;
        float ShakeTime;

        Vector2 mapSize;

        void Start()
        {
            playerTransform = GameManager.inst.player.transform;
            height = Camera.main.orthographicSize;
            width = height * Screen.width / Screen.height;
            mapSize = GameManager.inst.map.mapSize;
            center = GameManager.inst.map.center;
        }

        void Update()
        {
            if (ShakeTime > 0 && GameManager.inst.isLive)
            {
                transform.position = Random.insideUnitSphere * ShakeAmount + transform.position;
                ShakeTime -= Time.deltaTime;
            }
            else
            {
                ShakeTime = 0.0f;
            }
        }

        void FixedUpdate()
        {
            LimitCameraArea();
        }

        void LimitCameraArea()
        {
            transform.position = Vector3.Lerp(transform.position,
                                              playerTransform.position + cameraPosition,
                                              Time.deltaTime * cameraMoveSpeed);
            float lx = mapSize.x - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

            float ly = mapSize.y - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

            transform.position = new Vector3(clampX, clampY, -10f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, mapSize * 2);
        }

        public void VibrateForTime(float time)
        {
            ShakeTime = time;
        }
    }
}

