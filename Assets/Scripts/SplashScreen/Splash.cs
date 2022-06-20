using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{


    [SerializeField] private Image backImage;
    [SerializeField] private Image logoImage;
    [SerializeField] RectTransform fader;


    private void Awake()
    {
        logoImage.gameObject.transform.localScale = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        backImage.gameObject.LeanScale(Vector3.one, 5f).setDelay(1f).setEaseLinear();
        logoImage.gameObject.LeanScale(Vector3.one, 5f).setDelay(1f).setEaseLinear().setOnComplete(() =>
        {
            
            StartCoroutine(TransitionScene(StringManifest.Scene_OnBoarding));
        });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    public IEnumerator TransitionScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        
        fader.gameObject.SetActive (true);
    
           
        // ALPHA
             
        LeanTween.alpha (fader, 0, 0);
        LeanTween.alpha (fader, 1, 0.5f).setOnComplete (() => {
            
            SceneManager.LoadScene (sceneName);
            
        });
        
    }
    
    
    public static void _ShowAndroidToastMessage(string message)
    {
        
#if UNITY_ANDROID && !UNITY_EDITOR
        
        AndroidJavaClass unityPlayer =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>(
                    "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }




#endif
        
        
        
    }
}
