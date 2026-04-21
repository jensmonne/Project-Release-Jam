using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private int MaxRoundCount = 3;
    [SerializeField] private int CurrentRoundCount = 0;
    [SerializeField] private float RoundDuration = 60f;
    [SerializeField] private float ItemSpawnInterval = 5f;

    [Header("Map Settings")]
    [SerializeField] private GameObject[] maps;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerSpawnPoints;
    [SerializeField] private GameObject[] itemSpawnPoints;


    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        CurrentRoundCount = 0;
        StartRound();
        // Additional logic to start the game can be added here
    }

    private void StartRound()
    {
        CurrentRoundCount++;
        //Pick random map or choose map
        //SpawnPlayers
        //Start item spawn Cycle
        StartItemSpawnCycle();
        //Start round timer
        //Countdown for round to start
    }

    private void StartItemSpawnCycle()
    {
        StartCoroutine(SpawnItemDelay());
        // Logic to spawn items at intervals can be added here
    }

    private IEnumerator SpawnItemDelay()
    {
        yield return new WaitForSeconds(ItemSpawnInterval);
    }

    public void EndRound()
    {
        CurrentRoundCount++;
        if (CurrentRoundCount >= MaxRoundCount)
        {
            EndGame();
        }
        else
        {
            StartRound();
            // Logic to start the next round can be added here
        }
    }

    public void EndGame()
    {
        //Scene switch to result scene
        // Additional logic to end the game can be added here
        Debug.Log("Game is done!");
    }
}


