using System;
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
        level1.enabled = false;
        scoreText = Score.GetComponentInChildren<Text>();
        MusicController.SoundController(MusicController.SOUNDS.MENU, true);
    }

    public void RunLevel1() {
        level1.Run();
        level1.enabled = true;
        MainMenu.SetActive(false);
        Score.SetActive(true);
        MusicController.SoundController(MusicController.SOUNDS.MENU, false);
        MusicController.SoundController(MusicController.SOUNDS.GAME_INTRO, true);
    }

    public void RestartLevel1() {
        level1.mcMovement.Stop();
        StartCoroutine(DelayRestart());
    }
    
    IEnumerator DelayRestart() {
        yield return new WaitForSeconds(4f);
        resetScore();
        level1.Restart();
    }

    private void resetScore() {
        score = -1;
        AddScore(1);
    }

    public void BackToMenu() {
        level1.enabled = false;
        MainMenu.SetActive(true);
        Score.SetActive(false);
    }

    public void AddScore(int add) {
        score += add;
        scoreText.text = String.Format("{0:000000}", score);
    }
}
