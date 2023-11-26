using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManeger2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void retrybutton()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void GameOverhomebutton()
    {
        SceneManager.LoadScene("Home");
    }
}
