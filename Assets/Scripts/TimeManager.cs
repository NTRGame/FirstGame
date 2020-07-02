﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System;
using Assets;

public class TimeManager : MonoBehaviour
{
    public Text timekeepingText;
    private int timekeeping;

    private ElapsedEventHandler eventHandler;
    public event ElapsedEventHandler TimeCallBack
    {
        add
        {
            this.eventHandler += value;
        }

        remove
        {
            this.eventHandler -= value;
        }
    }

    protected void OnTimeCallBack()
    {
        if (this.eventHandler!= null)
        {
            EventArgs e = new EventArgs();
            Timer timer = new Timer();
            timer.Interval = 30000;
            timer.Elapsed += eventHandler;
            timer.Start();
        }
    }
    void Start()
    {
        timekeepingText.text = "30";
        timekeeping = int.Parse(timekeepingText.text);
        TimeRefresh();
        this.TimeCallBack += TimeManager_TimeCallBack;
        TimeStart();

    }

    private void TimeManager_TimeCallBack(object sender, ElapsedEventArgs e)
    {
        timekeeping = 30;
    }

    void Update()
    {
        timekeepingText.text = timekeeping.ToString();
    }

    public void TimeRefresh()
    {
        Timer timer = new Timer();
        timer.Interval = 1000;
        timer.Elapsed += TimeRefreshAction;
        timer.Start();
    }

    private void TimeRefreshAction(object sender, ElapsedEventArgs e)
    {
        timekeeping--;
    }

    public void TimeStart()
    {
        OnTimeCallBack();
    }

}
class Attribute
{
    private float LeftHp,RightHp;
    private float LeftMoney,RigthMoney;
    public float SetLeftHealth(float LeftHp)
    {
        return LeftHp ;
    }
    public float SetLeftMoney(float LeftMoney)
    {
        return LeftMoney;  
    }
    public float SetRightHealth(float RightHp)
    {
        return RightHp ;     
    }
    public float SetRightMoney(float RigthMoney)
    {
        return RigthMoney;
    }
}

