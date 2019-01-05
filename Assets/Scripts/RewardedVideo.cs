using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedVideo : MonoBehaviour {

    private void Start() {
        AdinCube.Rewarded.Fetch();
    }

    public void ShowMyRewardedVideo() {
        AdinCube.Rewarded.Show();
    }

    void OnEnable() {
        AdinCubeRewardedEventManager.OnAdCompleted += OnAdCompleted;
    }

    void OnDisable() {
        AdinCubeRewardedEventManager.OnAdCompleted -= OnAdCompleted;
    }

    void OnAdCompleted() {
        FindObjectOfType<GameManager>().currentCash += 100;
        PlayerPrefs.SetInt("cash", FindObjectOfType<GameManager>().currentCash);
    }
}