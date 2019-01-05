using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    public Image bacgroundImg;

    public Sprite[] backgrounds;
    public Color[] colors;

    private void Start() {
        bacgroundImg.color = colors[Random.Range(0, colors.Length)];
        bacgroundImg.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }
}
