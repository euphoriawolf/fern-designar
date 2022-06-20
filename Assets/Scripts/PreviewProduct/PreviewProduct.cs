using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewProduct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BackClick()
    {
        GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_Homepage);
    }

    public void ViewIn3DClick()
    {
        GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_ARView);
    }
    
    
    
}
