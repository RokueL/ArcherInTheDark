using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerControl playerControl;
    public Text coins;
    public Text Die;


    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.Find("character").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = playerControl.Score.ToString();
        if(playerControl.hp <= 0)
        {
            Die.gameObject.SetActive(true);
        }
    }
}
