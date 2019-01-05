using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelModeManager : MonoBehaviour {

    public Text levelText;

    private void Start() {
        
    }

    private void Update() {
        levelText.text = "LEVEL " + FindObjectOfType<GameManager>().currentBestLevel;
    }
}
