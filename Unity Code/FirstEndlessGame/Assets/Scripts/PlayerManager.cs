using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public static bool gameOver;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);
    }

    private void IsGameOver(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
    private void OnEnable() => PlayerController.isGameOver += IsGameOver;
    private void OnDisable() => PlayerController.isGameOver -= IsGameOver;
}
