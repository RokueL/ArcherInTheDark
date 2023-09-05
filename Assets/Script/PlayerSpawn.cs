using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{

    public GameObject Player;
    public Text Score;
    int Scoree;
    // Start is called before the first frame update
    void Start()
    {
        Player.transform.position = this.transform.position;
        Scoree = PlayerPrefs.GetInt("Score");
        Score.text = "Your Score : " + Scoree;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
