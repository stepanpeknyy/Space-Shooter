using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int score = 0;
    public int health = 5;
    private void Awake()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        int numberOfSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }
    public int GetHealth()
    {
        return health;
    }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }
    public void SubtractFromHealth(int healthValue)
    {
        health -= healthValue;
    }
    public void AddToHealth(int healthValue)
    {
        health += healthValue;
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
