/* Level Generator V2: Endless Generator
 * 
 * NOTE: Remember to set the Y position of the prefabs so that the tiles line up at the bottom
 *       Use yPosition = (yCoordinate + ((sizeOfPrefab - 1) / 2))
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_TILE = 100f;

    [SerializeField] private Transform[] dirt;
    [SerializeField] private GameObject player;

    private int lastTileHeight;
    private int lastTileXPos;

    private void Awake()
    { 
        lastTileHeight = 1;
        lastTileXPos = 8;

        for(int i = 0; i < 6; i++) 
        {
            spawnTile();
        }
    }

    private void spawnTile()
    {
        int rand = Random.Range(0, dirt.Length);
        Debug.Log(rand);
        
        if(rand != 0)
        {
            Instantiate(dirt[rand], new Vector3(lastTileXPos + 1, dirt[rand].position.y), Quaternion.identity);
        }

        lastTileXPos += 1;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = lastTileXPos - player.transform.position.x;
        Debug.Log("Tile: " + lastTileXPos);
        Debug.Log("Player" + player.transform.position.x);
        Debug.Log(distance);
        if(distance < PLAYER_DISTANCE_SPAWN_TILE)
        {
            spawnTile();
        }

        player = GameObject.Find("PlayerTemplate");
    }
}
