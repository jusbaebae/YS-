using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")) {
            //���ӸŴ��� ��ũ��Ʈ�� ���ο��� �߰�(���ӸŴ��� ��ũ��Ʈ�� Ǯ �Ŵ����߰��ϱ�)
            //GameManager.instance.pool.Get(0);
        }
    }
}
