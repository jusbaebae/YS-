using UnityEngine;

public enum Character
{
    Old_man,Old_woman,Man, Woman
}

[System.Serializable]
public class CharData : MonoBehaviour
{
    public RuntimeAnimatorController info_anim;
    public RuntimeAnimatorController play_anim;

    public Character character;
    public float hp;
    public float attack;
    public float defense;
    public float luck;

    public int speed;
}