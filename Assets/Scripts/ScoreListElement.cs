﻿using UnityEngine;
using TMPro;

public class ScoreListElement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI listNumber = null;

    [SerializeField]
    private TextMeshProUGUI nameText = null;

    [SerializeField]
    private TextMeshProUGUI scoreText = null;

    [SerializeField]
    private TextMeshProUGUI datePlayedText = null;

    public TextMeshProUGUI NameText { get => nameText; }
    public TextMeshProUGUI ScoreText { get => scoreText; }
    public TextMeshProUGUI DatePlayedText { get => datePlayedText; }
    public TextMeshProUGUI ListNumber { get => listNumber; }


}
