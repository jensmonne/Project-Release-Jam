using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    private Queue<int> levelQueue = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeLevelQueue();
    }

    private void InitializeLevelQueue()
    {
        List<int> indices = new();

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            indices.Add(i);
        }

        Shuffle(indices);

        foreach (int index in indices)
        {
            levelQueue.Enqueue(index);
        }
    }

    public void LoadNextLevel()
    {
        if (levelQueue.Count > 0)
        {
            int nextLevelIndex = levelQueue.Dequeue();
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(0); // most likely to be main menu but might want to change this to a win screen or smth
        }
    }

    private void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}