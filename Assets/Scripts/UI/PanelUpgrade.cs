using System;
using System.Collections;
using System.Collections.Generic;
using Survival;
using UnityEngine;

public class PanelUpgrade : MonoBehaviour
{
    public ShootBullet shootBullet;
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void OnClickBtn1()
    {
        shootBullet.shootInterval = 0.5f;
        shootBullet.RestartSpawnBuller();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
