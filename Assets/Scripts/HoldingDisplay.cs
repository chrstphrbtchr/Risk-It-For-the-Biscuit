using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldingDisplay : MonoBehaviour
{
    public TMP_Text display;
    public Image bg;
    public bool haulNullThisFrame, haulNullLastFrame, hadRockLastFrame;
    Color originalBG;

        private void Start()
    {
        originalBG = bg.color;
    }

    void Update()
    {
        haulNullThisFrame = MouseLaunch.haul == null;
        if (haulNullThisFrame != haulNullLastFrame || hadRockLastFrame != MouseLaunch.HasRock )
        {
            if (haulNullThisFrame)
            {
                if (MouseLaunch.HasRock) { bg.color = originalBG; display.text = "HOLDING PEBBLE"; }
                else
                {
                    bg.color = Color.clear;
                    display.text = "HOLDING NOTHING";
                }
            }
            else
            {
                bg.color = originalBG;
                display.text = $"HOLDING {MouseLaunch.haul.displayName}";
            }
        }
        haulNullLastFrame = haulNullThisFrame;
        hadRockLastFrame = MouseLaunch.HasRock;
    }
}
