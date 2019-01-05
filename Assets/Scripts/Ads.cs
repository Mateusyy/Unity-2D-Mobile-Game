using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ads : MonoBehaviour {

    void Start() // Init SDK on app start
    {
        Screen.fullScreen = false;

        AdinCube.SetAndroidAppKey("c3077ccd797b451f96ca"); // replace with your Android app key

        AdinCube.UserConsent.Ask();                        // ask for consent for EU residents
        AdinCube.Interstitial.Init();                      // start caching interstitial ads

        //BANNER
        // display a banner ad
        AdinCube.Banner.Show(
            AdinCube.Banner.Size.BANNER_320x50,   // screen wide banner
            AdinCube.Banner.Position.TOP);      // at the top
    }

    void OnEnable() {
        // register to AdinCube user consent manager events
        AdinCubeUserConsentEventManager.OnAccepted += OnAccepted;
        AdinCubeUserConsentEventManager.OnDeclined += OnDeclined;
        AdinCubeUserConsentEventManager.OnError += OnError;
    }

    void OnDisable() {
        // unregister all event handlers
        AdinCubeUserConsentEventManager.OnAccepted -= OnAccepted;
        AdinCubeUserConsentEventManager.OnDeclined -= OnDeclined;
        AdinCubeUserConsentEventManager.OnError -= OnError;
    }

    void OnAccepted() {
        //AdinCube.NativeLog("OnAccepted");
        //AdinCube.NativeToast("OnAccepted");
    }

    void OnDeclined() {
        // AdinCube.NativeLog("OnDeclined");
        // AdinCube.NativeToast("OnDeclined");
    }

    void OnError(string errorCode) {
        //  AdinCube.NativeLog("OnError: " + errorCode);
        //  AdinCube.NativeToast("OnError: " + errorCode);
    }
}
