using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateGames;

public class CubeSurferLevel : LevelManager
{

    private new void Awake()
    {
        base.Awake();
    }

    public override void FinishLevel(bool success)
    {
        GameManager.Instance.FinishLevel(success);
    }

    public override void StartLevel()
    {
        print("start lvl");
    }

}