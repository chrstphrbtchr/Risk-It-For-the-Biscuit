using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldingDisplay : MonoBehaviour
{
    public TMP_Text display;
    public Image bg;
    public bool haulNullThisFrame, haulNullLastFrame;
    Color originalBG;

        private void Start()
    {
        originalBG = bg.color;
    }

    void Update()
    {
        haulNullThisFrame = MouseLaunch.haul == null;
        if (haulNullThisFrame != haulNullLastFrame )
        {
            if (haulNullThisFrame)
            {
                bg.color = Color.clear;
                display.text = MouseLaunch.HasRock ? "HOLDING A ROCK" : "HOLDING NOTHING";
            }
            else
            {
                bg.color = originalBG;
                display.text = $"HOLDING {MouseLaunch.haul.displayName}";
            }
        }
        haulNullLastFrame = haulNullThisFrame;
    }
}
