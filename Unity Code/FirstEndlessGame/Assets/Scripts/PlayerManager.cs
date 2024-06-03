using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public static bool gameOver;
    public GameObject gameOverPanel;
    public TMP_Dropdown tmp_dropdown;
    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

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
        numberOfCoins = 0;
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);

        // populate available
        foreach (var availablePortName in System.IO.Ports.SerialPort.GetPortNames())
        {
            tmp_dropdown.options.Add(new TMP_Dropdown.OptionData(availablePortName));
        }
    }

    private void Update()
    {
        coinsText.text = "Coins: " + numberOfCoins;
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
