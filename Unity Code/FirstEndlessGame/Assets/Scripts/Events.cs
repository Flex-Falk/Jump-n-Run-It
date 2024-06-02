using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class Events : MonoBehaviour
{
    private PortDataAccessor portDataAccessor;

    [SerializeField] private TMP_Dropdown tmp_dropdown;
    [SerializeField] private TMP_InputField tmp_input;

    private void Start()
    {
        portDataAccessor = PortDataAccessor.Instance;

        // Set the port data to the first value of the dropdown menu, if an ESP32 is connected
        if (tmp_dropdown != null && tmp_dropdown.options.Count > 1)
        {
            //portDataAccessor.PortName = tmp_dropdown.options[1].text;
            tmp_dropdown.value = 1;
            HandleApplyPortSettings();
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HandleApplyPortSettings()
    {
        if (portDataAccessor != null)
        {
            portDataAccessor.PortName = tmp_dropdown.options[tmp_dropdown.value].text;
            portDataAccessor.Baudrate = int.Parse(tmp_input.text);

            portDataAccessor.CloseConnectionToPort();
            portDataAccessor.ConnectToPort();
            Debug.Log($"{portDataAccessor.PortName}, {portDataAccessor.Baudrate}");
        }
        else
        {
            Debug.Log("NULL !");
        }
    }

}
