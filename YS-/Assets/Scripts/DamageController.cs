using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private static DamageController _instance = null;

    public static DamageController instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DamageController>();
                if(_instance == null)
                {
                    //¤·¤µ¤·
                }
            }
            return _instance;
        }
    }
    public Canvas canvas;
    public GameObject dmgTxt;
    public GameObject critdmgTxt;

    public void CreateDamageText(Vector3 hitPoint, int Damage, bool critcheck)
    {
        if (critcheck)
        {
            GameObject damageTxt = Instantiate(critdmgTxt, hitPoint, Quaternion.identity, canvas.transform);
            damageTxt.GetComponent<Damage>().damage = Damage.ToString();
        }
        else
        {
            GameObject damageTxt = Instantiate(dmgTxt, hitPoint, Quaternion.identity, canvas.transform);
            damageTxt.GetComponent<Damage>().damage = Damage.ToString();
        }
        
    }
}
