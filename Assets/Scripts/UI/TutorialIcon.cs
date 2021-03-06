﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TutorialIcon
{
    SpriteRenderer targetObject;
    Animator animator;
    InputType type;

    public InputType Type { get => type; }

    public TutorialIcon(InputType pTutorialType)
    {
        GameObject canvas = GameObject.Find("TutorialImageCanvas");
        targetObject = canvas.GetComponent<SpriteRenderer>();
        animator = canvas.GetComponent<Animator>();

        SetTutorialType(pTutorialType);
    }

    void SetTutorialType(InputType tutorialType)
    {
        targetObject.color = new Color(1, 1, 1, 0.25f);
        type = tutorialType;
        animator.SetInteger("tutorialType", (int)tutorialType);
    }

    public void DisableTutorial()
    {
        targetObject.color = new Color(1, 1, 1, 0);
        animator.SetInteger("tutorialType", -1);
    }
}

public enum InputType
{
    SWIPE_TO_MOVE = 0,
    TAP_FIREFLY = 1,
    TAP_LANTERN = 2,
    TAP_BEETLE = 3,
    TAP_DRAGABLE = 4
}
