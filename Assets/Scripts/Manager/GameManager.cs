using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameManager instance; //static 공유변수. 어디서든 쉽체 참조 변경이 가능

    public GameObject bossPrefab;
    public Transform bossPosition;

    public PlayerController playerController;
    void Awake()
    {
        instance = this;
    }

    public void OnEndGame()
    {
        UIManager.instance.panelEnd.SetActive(true);
    }

    public void ShowBoss()
    {
        playerController.isInvincible = true;
        
        GameObject boss = Instantiate(bossPrefab, bossPosition.position, Quaternion.identity) as GameObject;

        
    }
}
