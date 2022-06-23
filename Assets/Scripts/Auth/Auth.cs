using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class Auth : MonoBehaviour
{

    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject signInPage;
    [SerializeField] private GameObject signUpPage;
    [SerializeField] private GameObject SignUpSuccessPanel;
    
    [SerializeField] private TMP_InputField emailSignIn, passwordSignIn;
    [SerializeField] private TMP_InputField firstnameSignUp, lastnameSignUp, emailSignUp, passwordSignUp , comfirmpasswordSignUp;
    
    private FirebaseAuth auth;
    private FirebaseUser userAuth;
    
    
    private string deviceID;
    
    
    // Start is called before the first frame update
    void Start()
    {
        signUpPage.SetActive(false);
        SignUpSuccessPanel.SetActive(false);
        
        auth = FirebaseAuth.DefaultInstance;
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



    // --- Sign In methods and instructions ----------
    
    
    public void SignInAuthClick()
    {
        // after signing in
        SignInFunc();
        
        
    }
    
    
    
    public void SignInFunc()
    {

       // WelcomeScript._ShowAndroidToastMessage("SignIn clicked");

        var _emailtext = emailSignIn.text;
        var _passwordtext = passwordSignIn.text;
        
        if (string.IsNullOrEmpty(_emailtext) && string.IsNullOrEmpty(_passwordtext))
        {
            //WelcomeScript._ShowAndroidToastMessage("Please fill the Email and Password entry");
        }
        else
        {
            //WelcomeScript._ShowAndroidToastMessage("This are the entered details: "+ _emailtext+" "+_passwordtext);
            StartCoroutine(Login(_emailtext, _passwordtext));

            
        }

        
    }

    private IEnumerator Login(string email, string password)
    {
        //var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to Login user with {registerTask.Exception}");
          
            // WelcomeScript._ShowAndroidToastMessage($"Failed to Login user with {registerTask.Exception}");
        }
        else
        {
            
            Debug.LogWarning($"Successfully logged in user {registerTask.Result.Email}");
            
            GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_Homepage);

            //WelcomeScript._ShowAndroidToastMessage($"Successfully logged in User {registerTask.Result.Email}");
            //SceneManager.LoadScene(StringHolder.Scene_ExperiencesScene);
        }
        



    }

    public void SignInMakePasswordVisible()
    {
        
    }



    // --------------- End ------------------------ 

    
    
    
    
    
    
    
    
    // -------- Sign Up methods and instructions ------------
    
    public void SignUpAuthClick()
    {
        //after signing up
        
        SignUpFunc();
       
    }

    public void SignUpContinueClick()
    {
        //after successful signup
        
        
        GetComponent<SceneTransition>().TransitionScene(StringManifest.Scene_Homepage);
    }


    public void SignUpFunc()
    {
        //WelcomeScript._ShowAndroidToastMessage("SignUp pressed");

        string name = "Mark John";
        
        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(emailSignUp.text) && string.IsNullOrEmpty(passwordSignUp.text))
        {
            //WelcomeScript._ShowAndroidToastMessage("Please fill in the missing entries");
        }
        else
        {
            if (passwordSignUp.text.Equals(comfirmpasswordSignUp.text))
            {
                StartCoroutine(Register(emailSignUp.text, passwordSignUp.text));

            }
            else
            {
                print("Password not the same");
            }
            //WelcomeScript._ShowAndroidToastMessage("Email entered:  " + emailSignUp.text);

            
        }
        
    }


    private IEnumerator Register(string email, string password)
    {
        //var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register user with {registerTask.Exception}");
            //WelcomeScript._ShowAndroidToastMessage($"Failed to register user with {registerTask.Exception}");
        }
        else
        {
            Debug.LogWarning($"Successfully Registered user {registerTask.Result.Email}");
            //WelcomeScript._ShowAndroidToastMessage($"Successfully Registered user {registerTask.Result.Email}");
            
            userAuth = registerTask.Result;
            
            //SaveUsername();
            
            OpenSignUpSuccessPanel();

        }

    }

    void OpenSignUpSuccessPanel()
    {
        
        SignUpSuccessPanel.SetActive(true);

        SignUpSuccessPanel.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        
        signInPage.SetActive(false);
        signUpPage.SetActive(false); 
    }
    
    public void SignUpMakePasswordVisible()
    {
     
        //TODO: Research on how to make Hidden Password visible
    }

    // ------------ End --------------------------
    
    
    
    
}
