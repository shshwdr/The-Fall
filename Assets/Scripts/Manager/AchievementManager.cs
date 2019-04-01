using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;


public class AchievementManager : Singleton<AchievementManager>
{
    public bool showLog = true;
    public string[] starStrings = { GPGSIds.achievement_twinkle_twinkle_little_star_i, GPGSIds.achievement_twinkle_twinkle_little_star_ii, GPGSIds.achievement_twinkle_twinkle_little_star_iii, GPGSIds.achievement_twinkle_twinkle_little_star_iv };
    public Dictionary<string, string> stageIdToAchievement = new Dictionary<string, string> (){ { "grass",GPGSIds.achievement_what_a_great_day_for_football_all_we_need_is_some_green_grass_and_a_ball },
        { "rain", GPGSIds.achievement_the_rainbow_is_a_promise } };
    public void IncrementAchievement(string str, int value, System.Action<bool> callback = null)
    {
            //(bool success) => {
            //  Debug.Log("(Lollygagger) Welcome Unlock: " + sssuccess);
            //})
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(
                str,
                value, (bool success) =>
                {
                    if (showLog)
                    {
                        Debug.Log("(The Fall) " + str + " Increment " + value + " : " +
                                  success);
                    }
                    if (callback != null)
                    {
                        callback(success);
                    }
                });
        }
    }

    public void UnlockAchievement(string str, float value = 100, System.Action<bool> callback = null)
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress(
                str, value, (bool success) =>
                {
                    if (showLog)
                    {
                        Debug.Log("(The Fall) " + str + " unlock " + value + " : " +
                                  success);
                    }
                    if (callback != null)
                    {
                        callback(success);
                    }
                });
        }
    }

    public void CollectStar(int starNum)
    {
        if (Social.localUser.authenticated)
        {
            foreach (string starString in starStrings)
            {
                IncrementAchievement(starString, starNum);
            }
        }
    }
}
