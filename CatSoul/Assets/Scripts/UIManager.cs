using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager _instance;

    public Image gameWin;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameWin()
    {
        gameWin.gameObject.SetActive(true);
    }

    public void BackToWelcome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Welcome");
    }
}
