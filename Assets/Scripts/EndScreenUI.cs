﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Touch;
using UnityEngine.Rendering;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField]
    private NewHighScoreUI newHighscoreUI = null;

    [SerializeField]
    private EndOverviewUI endOverviewUI = null;

    [SerializeField]
    private ScoreListUI scoreList = null;

    private void Awake()
    {
        newHighscoreUI.gameObject.SetActive(false);
        endOverviewUI.gameObject.SetActive(false);

        if (Highscore.checkHighscoreType(PlayerInfo.Score) == Highscore.HighscoreType.ALLTIME || Highscore.checkHighscoreType(PlayerInfo.Score) == Highscore.HighscoreType.DAILY)
        {
            newHighscoreUI.gameObject.SetActive(true);
        }
        else
        {
            SwitchToOverview();
        }
    }

    public void SwitchToOverview()
    {
        scoreList.LoadAndShowScore();
        newHighscoreUI.gameObject.SetActive(false);
        endOverviewUI.gameObject.SetActive(true);
    }

    public void Update()
    {

    }

}