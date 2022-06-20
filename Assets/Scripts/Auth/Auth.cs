using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auth : MonoBehaviour
{

    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject signInPage;
    [SerializeField] private GameObject signUpPage;
    [SerializeField] private GameObject SignUpSuccessPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        signUpPage.SetActive(false);
        SignUpSuccessPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CancelClick()
    {
        Application.Quit();
    }

    public void SignInPageClick()
    {
        //var position = slider.transform.position;
        
        signInPage.SetActive(true);
        
        
        slider.transform.LeanMoveLocalX(-170f, 0.5f);

        signUpPage.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        signInPage.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        
        signUpPage.SetActive(false);
        SignUpSuccessPanel.SetActive(false);





    }

    public void SignUpPageClick()
    {
        
        signUpPage.SetActive(true);
        /*var position = slider.transform.position;
        slider.transform.localPosition = new Vector3(170,position.y,position.z);*/
        
        slider.transform.LeanMoveLocalX(170f, 0.5f);
        
        signInPage.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        signUpPage.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        
        signInPage.SetActive(false);
        SignUpSuccessPanel.SetActive(false);

    }


    public void SignInAuthClick()
    {
        // after signing in
        GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_Homepage);
    }

    public void SignUpAuthClick()
    {
        //after signing up
        
        SignUpSuccessPanel.SetActive(true);

        SignUpSuccessPanel.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        
        signInPage.SetActive(false);
        signUpPage.SetActive(false);
    }

    public void SignUpContinueClick()
    {
        //after successful signup
        
        
        GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_Homepage);
    }
}
