using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public ShopElement[] elements;
    [HideInInspector]
    public ShopElement element;

    public GameObject panelWithDetails;
    public GameObject buttonsToBuy;
    public GameObject buttonsDontHaveEnaughMoney;
    public Text panelWithDetails_text;



    private void Start() {

        //sprawdza zapisane dane czy piłka jest już kupiona
        //ustawia dane gdy piłka jest już kupiona lub gdy nie jest
        for (int i = 0; i < elements.Length; i++) {
            if(PlayerPrefs.GetInt("elements_"+i, 0) == 1) {
                elements[i].bought = true;
                elements[i].ballSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
        

        panelWithDetails.SetActive(false);
    }

    private void Update() {
        //sprawdzanie outline
        int myIndex = PlayerPrefs.GetInt("choiceElement", 0);
        for (int i = 0; i < elements.Length; i++) {
            if(i == myIndex) {
                elements[i].button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                //
                FindObjectOfType<BallManager>().currentBall = FindObjectOfType<BallManager>().balls[i];
            } else {
                elements[i].button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }
        
    }

    public void ShopElementAction(int index) {
        element = Array.Find(elements, e => e.index == index);

        //nie kupiony
        if (!element.bought) {
            if(element.prize <= FindObjectOfType<GameManager>().currentCash) {
                panelWithDetails.SetActive(true);
                buttonsToBuy.SetActive(true);
                buttonsDontHaveEnaughMoney.SetActive(false);
                panelWithDetails_text.text = "Do you want buy  this Ball for  $" + element.prize + "?";
            } else {
                panelWithDetails.SetActive(true);
                buttonsToBuy.SetActive(false);
                buttonsDontHaveEnaughMoney.SetActive(true);
                panelWithDetails_text.text = "You do not have enough money!";
            }
        } else {    //kupiony
            for (int i = 0; i < elements.Length; i++) {
                if(elements[i].index == index) {
                    elements[i].button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    PlayerPrefs.SetInt("choiceElement", i);
                    //
                    FindObjectOfType<BallManager>().currentBall = FindObjectOfType<BallManager>().balls[i];
                } else {
                    elements[i].button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }
        }
    }

    public void BuyConfirmButton() {
        if (element != null) {
            //kasa
            FindObjectOfType<GameManager>().currentCash -= element.prize;
            PlayerPrefs.SetInt("cash", FindObjectOfType<GameManager>().currentCash);
            //obrazek
            element.ballSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //ballInGame
            //
            FindObjectOfType<BallManager>().currentBall = FindObjectOfType<BallManager>().balls[element.index];
            //checkbox
            element.bought = true;
            //PlayerPrefs
            PlayerPrefs.SetInt("elements_"+element.index, 1);
            //outline
            PlayerPrefs.SetInt("choiceElement", element.index);
        }

        //ukrycie panelu
        ClosePanelButton();
    }

    public void ClosePanelButton() {
        
        buttonsToBuy.SetActive(false);
        buttonsDontHaveEnaughMoney.SetActive(false);
        panelWithDetails.SetActive(false);
    }
}
