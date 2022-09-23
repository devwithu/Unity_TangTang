using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameManager instance; //static 공유변수. 어디서든 쉽체 참조 변경이 가능

    void Awake()
    {
        instance = this;
    }

    public void OnEndGame()
    {
        UIManager.instance.panelEnd.SetActive(true);
    }
}
