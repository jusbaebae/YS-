using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace vanilla
{
    public class DropItem : MonoBehaviour
    {
        public void RandDrop()
        {
            GameManager.inst.Dpool.Get(1);
        }
    }
}



