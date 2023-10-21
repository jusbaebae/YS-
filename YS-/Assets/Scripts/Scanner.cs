using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class Scanner : MonoBehaviour
    {
        public float scanRange;
        public LayerMask targetLayer;
        public RaycastHit2D[] targets;
        public Transform[] nearestTarget = new Transform[10];
        private void FixedUpdate()
        {
            targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
            nearestTarget = GetNearest();
        }

        Transform[] GetNearest()
        {
            Transform[] result = new Transform[10];
            float diff = 100;
            int i = 0;
            foreach (RaycastHit2D target in targets)
            {
                Vector3 myPos = transform.position;
                Vector3 targetPos = target.transform.position;
                float curDiff = Vector3.Distance(myPos, targetPos);

                if (curDiff < diff && target.transform != null)
                {
                    diff = curDiff;
                    if (i < 10)
                    {
                        result[i] = target.transform;
                        i++;
                    }
                }
            }

            return result;
        }
    }
}
