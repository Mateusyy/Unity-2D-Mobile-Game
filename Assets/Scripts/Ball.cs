using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject touchEffect;

    private Rigidbody2D rb;
    public float speed = 1f;
    public static float globalGravity = -9.8f;
    public float gravityScale = 1.0f;
    private bool isForce = false;

    public float heightJump = 5f;

    private void Start() {

        gameObject.GetComponent<SpriteRenderer>().sprite = FindObjectOfType<BallManager>().currentBall;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate() {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode2D.Force);
    }

    private void Force() {
        isForce = false;
        rb.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
        rb.velocity = new Vector3(0f, heightJump, 0f);
    }

    private void Update() {
        if (isForce == true)
            Force();

    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.tag == "PlatformGame") {
            GameObject touchEffectGO = (GameObject)Instantiate(touchEffect, transform.position, Quaternion.identity);
            Destroy(touchEffectGO, 1f);
            isForce = true;
        }
        
        if(collider.tag == "EnemyPlatformGame") {
            FindObjectOfType<GameManager>().isGame = false;

            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            GameObject touchEffectGO = (GameObject)Instantiate(touchEffect, transform.position, Quaternion.identity);
            touchEffectGO.GetComponent<ParticleSystem>().startColor = Color.red;
            Destroy(touchEffectGO, 1f);

            if(FindObjectOfType<GameManager>().bestLevelPoints < FindObjectOfType<GameManager>().currentLevelPoints) {
                PlayerPrefs.SetInt("bestScore", FindObjectOfType<GameManager>().currentLevelPoints);
            }
            PlayerPrefs.SetInt("cash", FindObjectOfType<GameManager>().currentCash);

            FindObjectOfType<GameManager>().MenuPanel.SetActive(true);
            FindObjectOfType<GameManager>().MenuPanel.transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<GameManager>().MenuPanel.transform.GetChild(1).gameObject.SetActive(false);
            //FindObjectOfType<SceneFader>().FadeTo(Application.loadedLevelName);
        }

        if(collider.tag == "CheckPoint") {
            FindObjectOfType<GameManager>().AddPoints(10);
            FindObjectOfType<SceneFader>().FateTextAddPoint();
        }

        if(collider.tag == "Money") {
            FindObjectOfType<GameManager>().currentCash += 1;
            collider.gameObject.SetActive(false);
        }

        if(collider.tag == "FinishLine") {
            PlayerPrefs.SetInt("bestLevel", FindObjectOfType<GameManager>().currentBestLevel + 1);
            Debug.Log("Next Level");
            FindObjectOfType<GameManager>().MenuPanel.SetActive(true);
            FindObjectOfType<GameManager>().MenuPanel.transform.GetChild(0).gameObject.SetActive(false);
            FindObjectOfType<GameManager>().MenuPanel.transform.GetChild(1).gameObject.SetActive(false);
            FindObjectOfType<GameManager>().MenuPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
