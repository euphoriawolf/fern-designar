using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoarding : MonoBehaviour
{

    [SerializeField] private List<GameObject> Slides;
    private int currentNo = 0;
    private int prevNo = 0;
    private int listCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        listCount = Slides.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SkipClick()
    {
        currentNo = listCount - 1;
        
        GameObject slide = Slides[currentNo];
        GameObject prevSlide = Slides[prevNo];

        prevNo = currentNo;

        prevSlide.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        slide.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);

    }

    public void NextClick()
    {
        if (currentNo >= listCount)
        {
            currentNo = 0;
            return;
        }
            
        
        currentNo += 1;
        
        GameObject slide = Slides[currentNo];
        GameObject prevSlide = Slides[prevNo];

        prevNo = currentNo;

        prevSlide.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        slide.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
    }

    public void GetStartedClick()
    {
       //TODO: Do some checking here with the firebase database and authentication
       GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_Auth);
    }


}
