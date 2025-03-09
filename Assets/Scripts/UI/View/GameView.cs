using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : View
{
    public SliderBar PlayerHealth;
    public SliderBar PlayerExp;
    public TMP_Text PlayerLevel;
    public TMP_Text PlayerLevelFade;

    public override void Init()
    {
        base.Init();
    }

    public override void OpenView(bool _instant = false, float timeToOpen = 0.2F)
    {
        base.OpenView(_instant, timeToOpen);
    }

    public override void CloseView()
    {
        base.CloseView();
    }

    public void SetLevel(int level)
    {
        PlayerLevel.text = "Level : " + (level + 1);
        PlayerLevelFade.text = "Level : " + (level + 1);
    }
}
