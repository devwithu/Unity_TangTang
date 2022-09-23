using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PreloadKeyStorePass
{
#if UNITY_EDITOR
 
    static PreloadKeyStorePass() {
 
        PlayerSettings.Android.keystorePass = "secret";
        PlayerSettings.Android.keyaliasName = "imsi"; 
        PlayerSettings.Android.keyaliasPass = "secret";
    }
 
#endif
}
