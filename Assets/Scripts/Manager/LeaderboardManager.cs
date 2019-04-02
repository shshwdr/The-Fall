using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class LeaderboardManager : Singleton<LeaderboardManager>
{
    public bool showLog = true;
    public void ReportScore(string str,int value, System.Action<bool> callback = null)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Note: make sure to add 'using GooglePlayGames'
            PlayGamesPlatform.Instance.ReportScore(value,
                str,
                (bool success) =>
                {
                    if (showLog)
                    {
                        Debug.Log("(The Fall) Leaderboard update "+str+" "+value+" success: " + success);
                    }
                    if (callback != null)
                    {
                        callback(success);
                    }
                });
        }
    }
}
