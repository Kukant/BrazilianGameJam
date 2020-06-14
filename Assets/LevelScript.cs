using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelScript : MonoBehaviour {

    public GameObject MovingBackgroundPrefab, PlayerPrefab;
    public List<GameObject> EnemiesPrefabs;
    public int enemiesPerIntervalBase = 4;


    private GameObject mainCamera;
    private CameraMovement mcMovement;
    private GameObject movingBackground, player;
    private Vector3 initialPos;


    private float enemiesGenerationLength = 50;
    private float generationOffest = 30;
    private float lastCameraPos;
    private int generations = 0;
    private float generationRangeY = 4;
    
    public void Start() {
        initialPos = transform.position;
        Load();
        Run();
    }
    
    public void Load() {
        player = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        movingBackground = Instantiate(MovingBackgroundPrefab, transform.position, Quaternion.identity);
        generateEnemies(generationOffest, enemiesGenerationLength + generationOffest);
        
        mainCamera = GameObject.Find("Main Camera");
        Debug.Log(mainCamera);
        mcMovement = mainCamera.GetComponent<CameraMovement>();
        mcMovement.speed = 0;
        mcMovement.goldSpeed = 0f;

        lastCameraPos = 0f;
    }

    public void Run() {
        mcMovement.goldSpeed = 0.1f;
    }
    
    public void Restart() {
        Destroy(movingBackground);
        Destroy(player);
        
        transform.position = initialPos;
        mainCamera.transform.position = initialPos;
        
        Load();
        Run();
    }

    // Update is called once per frame
    void FixedUpdate() {
        var cameraPosX = mainCamera.transform.position.x;
        if (cameraPosX > lastCameraPos + enemiesGenerationLength) {
            generateEnemies(
                cameraPosX + generationOffest,
                cameraPosX + generationOffest + enemiesGenerationLength);
            lastCameraPos = cameraPosX;
        }
    }

    private void generateEnemies(float fromX, float toX) {
        Debug.Log($"{fromX} -> {toX}");
        float enemiesToGenerate = Convert.ToInt32(Random.value * enemiesPerIntervalBase) + generations;
        generations++;

        for (var i = 0; i < enemiesToGenerate; i++) {
            float x = Random.value * (toX - fromX) + fromX;
            float y = generationRangeY * Random.value * (Random.value > 0.5 ? 1 : -1);
            int enemIdx = Convert.ToInt32( Math.Floor(EnemiesPrefabs.Count * Random.value));
            Instantiate(EnemiesPrefabs[enemIdx], new Vector2(x, y), Quaternion.identity);
        }
    }
}
