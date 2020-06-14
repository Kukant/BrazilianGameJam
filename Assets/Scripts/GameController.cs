﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject Level1Prefab;

    private LevelScript level1;

    public GameObject MainMenu, Score;

    private int score = 0;
    private Text scoreText;
    
    // Start is called before the first frame update
    void Start() {
        level1 = Instantiate(Level1Prefab, Vector3.zero, Quaternion.identity).GetComponent<LevelScript>();
        level1.Load();
        scoreText = Score.GetComponentInChildren<Text>();
    }

    public void RunLevel1() {
        level1.Run();
        MainMenu.SetActive(false);
        Score.SetActive(true);
    }

    public void RestartLevel1() {
        resetScore();
        level1.Restart();
    }

    private void resetScore() {
        score = -1;
        AddScore(1);
    }

    public void AddScore(int add) {
        score += add;
        scoreText.text = String.Format("{0:000000}", score);
    }
}