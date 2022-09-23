using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Mirror;

public class PlayerInfo : MonoBehaviour
{
    public PlayerController playerController;

    public Text textNick;
    public Text textExp;
    public Text textTime;
    public Image imageBar;
    public float updateInfoInterval = 1f;
    
    private void Start()
    {
        textExp = GameObject.Find("TextExp").GetComponent<Text>();
        textTime = GameObject.Find("TextTime").GetComponent<Text>();
        UpdateNick();
    }
    
    public void ReapeatUpdateInfo()
    {
        InvokeRepeating("UpdateInfo", this.updateInfoInterval, this.updateInfoInterval);
    }

    void Update()
    {
        imageBar.fillAmount = (float) playerController.currentHealth / (float)playerController.maxHealth;
    }

    void UpdateInfo()
    {
        UpdateExp();
        UpdateNick();
        UpdateTime();
    }
    public void UpdateNick()
    {
        if( ! playerController.playerNick.Equals(textNick.text) )
            textNick.text = playerController.playerNick;
    }
    public void UpdateExp()
    {
        if( ! playerController.currentExp.ToString().Equals(textExp.text) )
            textExp.text = playerController.currentExp.ToString();
    }

    public void UpdateTime()
    {
        //Debug.Log(playerController.SurvivalTime());
        TimeSpan deltaTime = playerController.SurvivalTime();
        textTime.text = deltaTime.ToString( @"hh\:mm\:ss");

    }
}
