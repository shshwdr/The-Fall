using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayViewController : MonoBehaviour
{
    public GameObject signedInObject;
    public GameObject unSignedInObject;
    public GameObject moreItemsObject;
    int retryTime = 3;
    int currentRetry;
    bool hasOpenedMoreItems;
    //public Text authStatus;
    // Start is called before the first frame update
    void Start()
    {
        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        UpdateSignedInUI(false);
        moreItemsObject.SetActive(false);
        PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
    }
    public void UpdateSignedInUI(bool signed = true)
    {
        signedInObject.SetActive(signed);
        unSignedInObject.SetActive(!signed);
    }
    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(Lollygagger) Signed in!");

            // Change sign-in button text
            //GetComponent<Dropdown>().options[0].text = "Sign out";
            UpdateSignedInUI();
            // Show the user's name
            //authStatus.text = "Signed in as: " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("(Lollygagger) Sign-in failed...");
            currentRetry += 1;
            if (currentRetry <= retryTime)
            {
                PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
            }
            // Show failure message
            //GetComponent<Dropdown>().options[0].text = "Sign in";
            //authStatus.text = "Sign-in failed";
        }
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            currentRetry = 0;
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            //GetComponent<Dropdown>().options[0].text = "Sign In";
            //authStatus.text = "";
        }
    }

    public void SelectDropdown(int index)
    {
        switch (index)
        {
            case 0:
                SignIn();
                break;
            case 1:
                ShowLeaderboards();
                break;
            case 2:
                ShowAchievements();
                break;
            //case 3:
            //    SignIn();
            //    break;
            default:
                Debug.LogError("index " + index + " does not support in dropdown box");
                break;
        }
    }
    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }
    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.Log("Cannot show Achievements, not logged in");
        }
    }

    public void ToggleMoreItems()
    {
        moreItemsObject.SetActive(!hasOpenedMoreItems);
        hasOpenedMoreItems = !hasOpenedMoreItems;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
