using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameSceneMove()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndSceneMove()
    {
        SceneManager.LoadScene("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
