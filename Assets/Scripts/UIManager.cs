using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public int interval = 30;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                //则创建一个
                _instance = GameObject.Find("BG").GetComponent<UIManager>();
            //返回这个实例
            return _instance;
        }
    }

    public Text timekeepingText;
    private int timekeeping;

    public delegate void TimeCallBackDelegate();
    private event TimeCallBackDelegate TimeCallBack;
    public event TimeCallBackDelegate Time30;

    void Start()
    {
        timekeepingText.text = "30";
        timekeeping = int.Parse(timekeepingText.text);
        StartCoroutine(Timer());
        TimeCallBack += TimeTextUpdata;
        timekeeping = 30;
    }

    private void TimeTextUpdata()
    {
        timekeepingText.text = timekeeping.ToString();
    }

    private IEnumerator Timer()
    {
        float intervalTime = 1f;
        while (true)
        {
            intervalTime -= Time.deltaTime;
            if(intervalTime <= 0)
            {
                intervalTime = 1f;
                TimeCallBack?.Invoke();
                timekeeping = timekeeping > 0 ? timekeeping - 1 : interval;
                if(timekeeping == interval)
                {
                    Time30?.Invoke();
                }
            }
            yield return null;
        }
    }

}

