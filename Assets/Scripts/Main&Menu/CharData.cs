using UnityEngine;

public enum Character
{
    Default
}

[System.Serializable]
public class CharData : MonoBehaviour
{
    public RuntimeAnimatorController info_anim;
    public RuntimeAnimatorController play_anim;

    public Character character;
    public float hp;
    public float attack;
    public int speed;
}