﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmp;

    [SerializeField]
    PlayerManager playerInfo;

    string uiText = "Health: ";

    private void Update()
    {
        if (playerInfo.Health > 0)
            tmp.text = uiText + playerInfo.Health;
        else
            tmp.text = "game over!";
    }
}
