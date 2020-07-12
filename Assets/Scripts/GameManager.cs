using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    //兵种数量
    public Dictionary<SoldierType, int> LeftValue;
    public Dictionary<SoldierType, int> RightValue;

    public Text LeftMoneyObject, RightMoneyObject;

    private int leftMoney=100, rightMoney=100;

    public int LeftMoney
    {
        get { return leftMoney; }
        set
        {
            StartCoroutine(NumChange(LeftMoneyObject, value - leftMoney));
            leftMoney = value;
        }
    }

    public int RightMoney
    {
        get { return rightMoney; }
        set
        {
            StartCoroutine(NumChange(RightMoneyObject, value - rightMoney));
            rightMoney = value;
        }
    }

    private IEnumerator NumChange(Text text, int change)
    {
        if(change == 0)
        {
            yield return null;
        }
        int changeValue = change>0? -1:1;
        for (int i = change; i != 0; i+=changeValue)
        {
            text.text = (int.Parse(text.text) - changeValue).ToString();
            yield return null;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                //则创建一个
                _instance = GameObject.Find("BG").GetComponent<GameManager>();
            //返回这个实例
            return _instance;
        }
    }

    /// <summary>
    /// 添加要生成的部队
    /// </summary>
    public void AddSoldier()
    {
        var button = EventSystem.current.currentSelectedGameObject;
        
        foreach (string soldierType in Enum.GetNames(typeof(SoldierType)))
        {
            if (soldierType.Equals(button.name))
            {
                
                if (button.transform.parent.name.Equals("Left"))
                {
                    if(LeftMoney >= 50)
                    {
                        LeftMoney = LeftMoney - 50;
                        int count = int.Parse(button.transform.GetChild(1).GetComponent<Text>().text);
                        count = count + 1;
                        button.transform.GetChild(1).GetComponent<Text>().text = count.ToString();
                        LeftValue[(SoldierType)Enum.Parse(typeof(SoldierType), button.name)] = count;
                    }                   
                }
                else
                {
                    if (RightMoney >= 50)
                    {
                        RightMoney = RightMoney - 50;
                        int count = int.Parse(button.transform.GetChild(1).GetComponent<Text>().text);
                        count = count + 1;
                        button.transform.GetChild(1).GetComponent<Text>().text = count.ToString();
                        RightValue[(SoldierType)Enum.Parse(typeof(SoldierType), button.name)] = count;
                    }
                }
            }
        }

    }

    public IEnumerator LeftCreateSoldier()
    {
        lock (LeftValue)
        {
            LeftMoney += 100;
            RightMoney += 100;
            GameObject FlagZombie = Resources.Load("Prefabs/FlagZombie", typeof(GameObject)) as GameObject;
            foreach (var keyValue in LeftValue)
            {
                for (int i = 0; i < keyValue.Value; i++)
                {
                    var basePostion = LeftPlayer.transform.GetChild(0).position;
                    var random = UnityEngine.Random.insideUnitCircle * 5;
                    basePostion += new Vector3(Mathf.Abs(random.x), random.y);
                    var soldier = Instantiate(FlagZombie, basePostion, new Quaternion(0f, 1f, -0.3f, 0), LeftPlayer.transform.GetChild(1).transform);
                    soldier.GetComponent<SoldierManager>().Init(XMLTools.GetSoldierByType(keyValue.Key));
                    soldier.name = keyValue.Key.ToString() + "---" + Time.time;
                    yield return null;
                }
            }
        }       
    }

    public IEnumerator RightCreateSoldier()
    {
        lock (RightValue)
        {
            LeftMoney += 100;
            RightMoney += 100;
            GameObject FlagZombie = Resources.Load("Prefabs/FlagZombie", typeof(GameObject)) as GameObject;
            foreach (var keyValue in RightValue)
            {
                for (int i = 0; i < keyValue.Value; i++)
                {
                    var basePostion = RightPlayer.transform.GetChild(0).position;
                    var random = UnityEngine.Random.insideUnitCircle * 5;
                    basePostion += new Vector3(-Mathf.Abs(random.x), random.y);
                    var soldier = Instantiate(FlagZombie, basePostion, new Quaternion(0.3f, 0f, 0f, 1f), RightPlayer.transform.GetChild(1).transform);
                    soldier.GetComponent<SoldierManager>().Init(XMLTools.GetSoldierByType(keyValue.Key));
                    soldier.name = keyValue.Key.ToString() + "---" + Time.time;
                    yield return null;
                }
            }
        }
    }

    public void CreateSoldier()
    {
        StartCoroutine(LeftCreateSoldier());
        StartCoroutine(RightCreateSoldier());
    }

    public GameObject LeftPlayer,RightPlayer;

    public Soldier LeftHome, RightHome;
    public Image RightHealthy, LeftHealthy;

    // Start is called before the first frame update
    void Start()
    {
        LeftValue = new Dictionary<SoldierType, int>();
        RightValue = new Dictionary<SoldierType, int>();
        UIManager.Instance.Time30 += CreateSoldier;
    }

    // Update is called once per frame
    void Update()
    {
        //更新血量
        RightHealthy.transform.localScale = new Vector3(RightHome.Healthy/100f, 1, 1);
        LeftHealthy.transform.localScale = new Vector3(LeftHome.Healthy / 100f, 1, 1);
    }
}
