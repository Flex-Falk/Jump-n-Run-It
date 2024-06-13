using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public bool gameOver;
    public List<GameObject> prefabList = new List<GameObject>();
    public GameObject gameOverPanel;
    public TMP_Dropdown tmp_dropdown;
    public int numberOfCoins;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreText;
    public float score;
    public static bool speedPowerUp;
    public bool shieldPowerUp;
    public static bool doubleJumpPowerUp;
    public Rigidbody rb;
    public AudioClip coinClip;
    public AudioClip breakClip;
    public GameObject shieldAnim;

    private AudioSource audioSource;

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
        score = 0f;
        speedPowerUp = false;
        shieldPowerUp = false;
        doubleJumpPowerUp = false;
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);


        audioSource = GetComponent<AudioSource>();


        // populate available
        foreach (var availablePortName in System.IO.Ports.SerialPort.GetPortNames())
        {
            tmp_dropdown.options.Add(new TMP_Dropdown.OptionData(availablePortName));
        }
    }

    public void Coin()
    {
        numberOfCoins++;
        audioSource.PlayOneShot(coinClip);
        coinsText.text = "Coins: " + numberOfCoins;
    }


    public void Crate(Transform trans)
    {
        GameObject prefab = prefabList[UnityEngine.Random.Range(0, prefabList.Count)];
        Vector3 v = new Vector3(trans.position.x, prefab.transform.position.y, trans.position.z);
        prefab.transform.position = v;
        Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        audioSource.PlayOneShot(breakClip);
    }


    public void Shield()
    {
        shieldAnim.SetActive(shieldPowerUp);
    }

    public void Score()
    {
        score = 0;    
        score += numberOfCoins;
        score += rb.position.z;
        score -= Time.timeSinceLevelLoad;
        if (score < 0){
            score = 0;
        }
        score = (int)Math.Ceiling(score);

        scoreText.text = "Score: " + score;
    }
    public bool HitShield()
    {
        //if it exists remove shield
        //if it doesnt, kill
        if (shieldPowerUp == true) {
            shieldPowerUp = false;
            Shield();
            return true;
        }else
        {
            return false; 
        }
        
    }

    private void IsGameOver(bool value)
    {
        if (value)
        {
            Score();
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

    }
    private void OnEnable() => PlayerController.isGameOver += IsGameOver;
    private void OnDisable() => PlayerController.isGameOver -= IsGameOver;
}
