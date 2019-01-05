using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public bool isGame;

    public int currentLevelPoints;
    public int currentCash;
    public int bestLevelPoints;
    public int currentBestLevel;
    public Text pointsTxt;
    public Text cashTxt;

    public GameObject MenuPanel;



    private void Awake() {
        isGame = true;
        MenuPanel.SetActive(false);

        
        bestLevelPoints = PlayerPrefs.GetInt("bestScore", 0);
        currentCash = PlayerPrefs.GetInt("cash", 0);
        currentBestLevel = PlayerPrefs.GetInt("bestLevel", 1);
        currentLevelPoints = 0;
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && isGame == true) {
            if(MenuPanel.activeSelf == false) {

                MenuPanel.SetActive(true);
                MenuPanel.transform.GetChild(0).gameObject.SetActive(false);
                MenuPanel.transform.GetChild(1).gameObject.SetActive(true);
            } else {
                MenuPanel.transform.GetChild(0).gameObject.SetActive(true);
                MenuPanel.transform.GetChild(1).gameObject.SetActive(false);
                MenuPanel.SetActive(false);
            }  
        }


        UpdateUI();
    }

    void UpdateUI() {
        if(Application.loadedLevelName != "_MENU_") {
            pointsTxt.text = " " + currentLevelPoints;
        } else {
            pointsTxt.text = "** best score **\n" + bestLevelPoints;
        }
        
        cashTxt.text = " $: " + currentCash;
    }

    public void AddPoints(int points) {
        currentLevelPoints += points;
    }

    public void ContinueButton() {
        MenuPanel.SetActive(false);
    }
}
