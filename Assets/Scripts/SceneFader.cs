using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public Image img;
    public Text addPointsTxt;

    void Start() {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene) {
        StartCoroutine(FadeOutAndLoad(scene));
    }

    public void FateTextAddPoint() {
        StartCoroutine(FadeText());
    }

    public void QuitApp() {
        Application.Quit();
    }

    IEnumerator FadeIn() {
        float t = 1f;

        while (t > 0f) {
            t -= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);

            yield return 0;
        }
    }

    IEnumerator FadeOutAndLoad(string scene) {
        float t = 0f;

        while (t < 1f) {
            t += Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);

            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

    IEnumerator FadeText() {
        float t = 0f;

        while (t < 0.2f) {
            t += Time.deltaTime;
            addPointsTxt.color = new Color(255f, 255f, 255f, t);

            yield return 0;
        }
        while(t > 0f) {
            t -= Time.deltaTime;
            addPointsTxt.color = new Color(255f, 255f, 255f, t);

            yield return 0;
        }
    }
}
