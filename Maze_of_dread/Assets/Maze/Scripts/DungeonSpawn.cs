using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawn : MonoBehaviour
{

    public GameObject Player;
    public GameObject PlayerSpawnRoom;
    public List<GameObject> bosses;
    public GameObject bossSpawnRoom;
    public List<GameObject> Enemies;
    public GameObject[] parentRooms;
    public int minEnemiesPerRoom = 1;
    public int maxEnemiesPerRoom = 3;
    public GameObject Book;
    public GameObject Sword;
    public GameObject Potion;
    public GameObject BookSpawnRoom;
    public GameObject SwordSpawnRoom;
    public GameObject PotionSpawnRoom;

    private GameObject player;
    private GameObject boss;
    private GameObject book;
    private GameObject sword;
    private GameObject potion;

    private void Start()
    {
        SpawnEntities();
    }
    

    private void SpawnEntities()
    {
        // Remove the BoxCollider from PlayerSpawnRoom if needed
        DestroyImmediate(PlayerSpawnRoom.GetComponent<BoxCollider>());

        // Spawn the player
        if (Player != null)
        { 
            //Vector3 playerSpawnPosition = new Vector3(68f, 1f, 60f);
            player = Instantiate(Player, PlayerSpawnRoom.transform.position, Quaternion.identity, transform);
        }

        //Spawn book
        if (Book != null)
        {
            book = Instantiate(Book, BookSpawnRoom.transform.position, Quaternion.identity, transform);
        }

        if (Sword != null)
        {
            sword = Instantiate(Sword, SwordSpawnRoom.transform.position, Quaternion.identity, transform);
        }

        if (Potion != null)
        {
            potion = Instantiate(Potion, PotionSpawnRoom.transform.position, Quaternion.identity, transform);
        }

        // Spawn a random boss if bosses are available
        if (bosses.Count > 0)
        {
            int randomBossIndex = Random.Range(0, bosses.Count);
            boss = Instantiate(bosses[randomBossIndex], bossSpawnRoom.transform.position, Quaternion.identity, bossSpawnRoom.transform);
            bossSpawnRoom.name = "Boss Room";
            boss.name = bosses[randomBossIndex].name;
        }

        // Spawn enemies if the list is not empty
        if (Enemies.Count > 0)
        {
            parentRooms = GameObject.FindGameObjectsWithTag("Room");
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        Debug.Log("Spawning enemies...");
        
        // Iterate through each room to spawn enemies
        for (int i = 0; i < parentRooms.Length; i++)
        {
            GameObject room = parentRooms[i];
            if (room == PlayerSpawnRoom || room == bossSpawnRoom) continue;

            // Calculate the room area for enemy spawn points
            int floorCount = 0;
            List<GameObject> floorTiles = new List<GameObject>();

            // Collect floor tiles in the room
            foreach (Transform child in room.transform)
            {
                if (child.CompareTag("Floor"))
                {
                    floorTiles.Add(child.gameObject);
                    floorCount++;
                }
            }

            // Determine the number of enemies to spawn in this room
            //int enemiesToSpawn = Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom);
            int enemiesToSpawn = Mathf.Min(Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom), floorTiles.Count);


            if(floorTiles.Count > 0){
                // Spawn each enemy randomly within the room bounds
            for (int j = 0; j < enemiesToSpawn; j++)
            {
                bool isSpawned = false;

                // Attempt to place an enemy until successfully placed
                while (!isSpawned)
                {
                    int randomTileIndex = Random.Range(0, floorTiles.Count);
                    GameObject floorTile = floorTiles[randomTileIndex];

                    if (!IsOccupied(floorTile))
                    {
                        Vector3 spawnPosition = floorTile.transform.position;
                        int randomEnemyIndex = Random.Range(0, Enemies.Count);
                        int randomRotation = Random.Range(0, 360);

                        GameObject enemy = Instantiate(
                            Enemies[randomEnemyIndex],
                            new Vector3(spawnPosition.x, spawnPosition.y + Enemies[randomEnemyIndex].transform.position.y, spawnPosition.z),
                            Quaternion.Euler(0, randomRotation, 0),
                            room.transform
                        );

                        enemy.name = Enemies[randomEnemyIndex].name;
                        isSpawned = true;
                    }
                }
            }
            }
            /*else if (floorTiles.Count == 0)
            {
                Debug.LogWarning("Skipping enemy spawn in room " + i + " - no floor tiles found.");
                continue;
            }*/

            
        }
    }

    // Helper function to check if a spawn point is already occupied
    private bool IsOccupied(GameObject floorTile)
    {
        
        return false; 
    }
}
