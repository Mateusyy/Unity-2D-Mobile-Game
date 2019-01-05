using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public Sprite[] balls;

    public int currentBallIndex;
    public Sprite currentBall;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        currentBallIndex = PlayerPrefs.GetInt("choiceElement", 0);
        currentBall = balls[currentBallIndex];

        Application.LoadLevel(1);
    }
}
