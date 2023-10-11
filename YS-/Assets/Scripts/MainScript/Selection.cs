using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharScript : MonoBehaviour
{

    [SerializeField] GameObject infoImage;

    private Sprite charImage;
    private GameObject character;

   
    void Update()
    {

    }

    public void CharPIck()
    {
        character = EventSystem.current.currentSelectedGameObject;
        charImage = character.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;

        infoImage.GetComponent<Image>().sprite = charImage;

    }
}
