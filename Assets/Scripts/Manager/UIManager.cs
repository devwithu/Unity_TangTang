using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public static UIManager instance; //static 공유변수. 어디서든 쉽체 참조 변경이 가능


    
    public GameObject panelNick;
    public GameObject panelEnd;
    
    public InputField inputFieldNick;
    public Button buttonStart;

    public PlayerController localPlayer;

    public string strExp;
    public string strSurvivalTime;
    
    public Text textExp;
    public Text textSurvivalTime;
    public Button buttonSetting;
    public Button buttonONBGM;
    public Button buttonOFFBGM;
    public Button buttonONSFX;
    public Button buttonOFFSFX;

    public Text textONBGM;
    public Text textOFFBGM;
    public Text textONSFX;
    public Text textOFFSFX;

    public Button buttonClearNick;
    public Button buttonClose;

    public GameObject gOSetting;

    public PlayerController player;
    
    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {

        buttonStart.onClick.AddListener(OnClickButtonStart);
        
        buttonSetting.onClick.AddListener(OnClickButtonSetting);
        buttonONBGM.onClick.AddListener(OnClickButtonONBGM);
        buttonOFFBGM.onClick.AddListener(OnClickButtonOFFBGM);
        buttonONSFX.onClick.AddListener(OnClickButtonONSFX);
        buttonOFFSFX.onClick.AddListener(OnClickButtonOFFSFX);
        buttonClose.onClick.AddListener(OnClickButtonClose);
        buttonClearNick.onClick.AddListener(OnClickButtonClearNick);

        gOSetting.SetActive(false);


        if (!PlayerPrefs.HasKey("PlayBMG") || PlayerPrefs.GetInt("PlayBMG") == 1)
        {
            SoundManager.instance.PlayerRandomBGM();

            var tempColor = textOFFBGM.color;
            tempColor.a = 0.5f;
            textOFFBGM.color = tempColor;
        
            tempColor = textONBGM.color;
            tempColor.a = 1f;
            textONBGM.color = tempColor;
        }
        else
        {
            var tempColor = textOFFBGM.color;
            tempColor.a = 1f;
            textOFFBGM.color = tempColor;
        
            tempColor = textONBGM.color;
            tempColor.a = 0.5f;
            textONBGM.color = tempColor;
        }

        if (!PlayerPrefs.HasKey("PlaySFX") || PlayerPrefs.GetInt("PlaySFX") == 1)
        {
            SoundManager.instance.playSFX = true;

            var tempColor = textOFFSFX.color;
            tempColor.a = 0.5f;
            textOFFSFX.color = tempColor;
        
            tempColor = textONSFX.color;
            tempColor.a = 1f;
            textONSFX.color = tempColor;
        }
        else
        {
            SoundManager.instance.playSFX = false;
            
            var tempColor = textOFFSFX.color;
            tempColor.a = 1f;
            textOFFSFX.color = tempColor;
        
            tempColor = textONSFX.color;
            tempColor.a = 0.5f;
            textONSFX.color = tempColor;
        }

        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        yield return new WaitForSeconds(1f);
        bool isShowBoss = false;
        
        while (true)
        {
            string strTempExp = "";
            string strTempSurvivalTime = "";

            strTempExp += player.currentExp;
            strTempSurvivalTime += player.SurvivalTime().ToString( @"mm\:ss") ;

            if (strTempSurvivalTime.Equals("00:00:00"))
            {
                if (!isShowBoss)
                {

                    isShowBoss = true;
                    Debug.Log("Sgsg");
                    GameManager.instance.ShowBoss();
                }

            }
            
            if (strTempSurvivalTime.Equals("00:00"))
            {
                if (!isShowBoss)
                {
                    isShowBoss = true;
                    Debug.Log("Sgsg");
                    GameManager.instance.ShowBoss();
                }
            }
            if (!strTempExp.Equals(strExp) )
            {
                strExp = strTempExp;
                textExp.text = strExp;
            }
            
            if ( !strTempSurvivalTime.Equals(strSurvivalTime))
            {
                strSurvivalTime = strTempSurvivalTime;
                textSurvivalTime.text = strSurvivalTime;
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
    
    void RpcUpdateRate()
    {
        textExp.text = strExp;
        textSurvivalTime.text = strSurvivalTime;
    }


    public void OnClickButtonStart()
    {

    }

    public void OnClickButtonONBGM()
    {
        SoundManager.instance.PlayerRandomBGM();
        
        PlayerPrefs.SetInt("PlayBMG", 1);
        PlayerPrefs.Save();
        
        var tempColor = textOFFBGM.color;
        tempColor.a = 0.5f;
        textOFFBGM.color = tempColor;

        tempColor = textONBGM.color;
        tempColor.a = 1f;
        textONBGM.color = tempColor;
        
        OnClickButtonClose();
    }

    public void OnClickButtonOFFBGM()
    {
        SoundManager.instance.PlayerStopBGM();
        
        PlayerPrefs.SetInt("PlayBMG", 0);
        PlayerPrefs.Save();
        
        var tempColor = textONBGM.color;
        tempColor.a = 0.5f;
        textONBGM.color = tempColor;
        
        tempColor = textOFFBGM.color;
        tempColor.a = 1f;
        textOFFBGM.color = tempColor;
        
        OnClickButtonClose();
    }

    public void OnClickButtonONSFX()
    {
        
        PlayerPrefs.SetInt("PlaySFX", 1);
        PlayerPrefs.Save();
        SoundManager.instance.playSFX = true;
        
        var tempColor = textOFFSFX.color;
        tempColor.a = 0.5f;
        textOFFSFX.color = tempColor;
        
        tempColor = textONSFX.color;
        tempColor.a = 1f;
        textONSFX.color = tempColor;
        
        OnClickButtonClose();
    }

    public void OnClickButtonOFFSFX()
    {
        PlayerPrefs.SetInt("PlaySFX", 0);
        PlayerPrefs.Save();
        SoundManager.instance.playSFX = false;
        
        var tempColor = textONSFX.color;
        tempColor.a = 0.5f;
        textONSFX.color = tempColor;
        
        tempColor = textOFFSFX.color;
        tempColor.a = 1f;
        textOFFSFX.color = tempColor;
        
        OnClickButtonClose();
    }

    public void OnClickButtonSetting()
    {
        gOSetting.SetActive(true);

    }
    public void OnClickButtonClose()
    {
        gOSetting.SetActive(false);
    }

    public void OnClickButtonClearNick()
    {
        PlayerPrefs.DeleteKey("PlayerNick");
        panelNick.gameObject.SetActive(true);
        
        gOSetting.SetActive(false);
    }
}
