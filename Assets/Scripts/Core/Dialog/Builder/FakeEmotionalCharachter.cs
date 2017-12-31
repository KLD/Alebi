﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeEmotionalCharachter : EmotionalCharacter
{
    public Text label;

    private void Start()
    {
        //ignore parent's Start()
    }

    public override void SetEmotion(string name)
    {
        label.text = name; 
    }
}