using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnTime = 0.5f;
    [SerializeField] float randomSpawnTime = 0.2f;
    [SerializeField] float movementSpeed = 2f;
    [SerializeField ] int numbersOfEnemies=5;

    public GameObject GetEnemyPrefab(){ return enemyPrefab; }
    public List<Transform> GetWaypoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform )
        {
            waveWaypoints.Add(child);
        }
        return  waveWaypoints; 
    } 
    public float GetSpawnTime() { return spawnTime; }
    public float GetRandomSpawnTime() { return randomSpawnTime; }
    public float GetMovementSpeed() { return movementSpeed; }
    public int GetNumbersOfEnemies() { return numbersOfEnemies; }
}
