using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Button startButton;
    public GameObject titleScreen;
    public bool isActive = false;
    public SpawnManager spawnManager;
    public GameObject player;
    public GameObject spawnObject;
    public TextMeshProUGUI enemyText;
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
    public void StartGame()
    {
        titleScreen.SetActive(false);
        player.SetActive(true);
        spawnObject.SetActive(true);
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        spawnManager.SpawnEnemyWave(spawnManager.waveNumber);
        isActive = true;
    }
    public void UpdateScore()
    {
        enemyText.text = "Enemies Left: " + spawnManager.enemyCount;
    }
}
