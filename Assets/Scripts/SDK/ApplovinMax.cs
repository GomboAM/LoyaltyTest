using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplovinMax : MonoBehaviour
{
    private void Start()
    {
        InitApplovin();
    }

    private void InitApplovin()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, start loading ads
            //MaxSdk.ShowMediationDebugger();
        };

        MaxSdk.SetSdkKey("9ZURTATZVEdg2tq3FuhiyvvpqnkVZ6bCFA8aZ6e6P2vDuAdSAh9CyeilTR5egVTrFETG3KQdxVRoOpu3MeThLO");
        MaxSdk.InitializeSdk();
    }
}
