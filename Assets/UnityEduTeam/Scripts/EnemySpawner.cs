using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField] List<GameObject> Enemies;
    [SerializeField] List<GameObject> EnemyPrefabs;
    [SerializeField] List<GameObject> EnemySpawnPoints;


    void Start()
    {
        EnemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").ToList();
        player = GameObject.FindGameObjectWithTag("Player");

        Enemies = new List<GameObject>();
        for (int i = 0; i < EnemySpawnPoints.Count; i++)
        {
           GameObject newEnemy = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)],
                EnemySpawnPoints[i].transform.position,
                EnemySpawnPoints[i].transform.rotation);
            Enemies.Add(newEnemy);

            newEnemy.GetComponent<SimpleAI>().player = player;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // vérification si un enemy est mort et le cas échéant en faire spawn un nouveau à une position aléatoire
        // pour cela on compare le nombre théorique d'enemy avec le nombre actuel
        while (EnemySpawnPoints.Count >
               Enemies.Count)
        {
            int RandomNumber = Random.Range(0, EnemySpawnPoints.Count);
            Instantiate(EnemyPrefabs[Random.Range(0, EnemySpawnPoints.Count)],
                EnemySpawnPoints[RandomNumber].transform.position,
                EnemySpawnPoints[RandomNumber].transform.rotation);
        }
    }
}
