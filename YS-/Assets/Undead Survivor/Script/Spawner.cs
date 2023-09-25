using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")) {
            //게임매니저 스크립트가 따로오면 추가(게임매니저 스크립트에 풀 매니저추가하기)
            //GameManager.instance.pool.Get(0);
        }
    }
}
