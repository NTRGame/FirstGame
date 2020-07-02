using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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

    public void CreateSoldier()
    {
        GameObject FlagZombie = Resources.Load("Prefabs/FlagZombie", typeof(GameObject)) as GameObject;
        //for(int i = 0; i < 1; i++)
        //{
        //    var basePostion = LeftPlayer.transform.GetChild(0).position;
        //    var random = Random.insideUnitCircle*5;
        //    basePostion += new Vector3(Mathf.Abs(random.x), random.y);
        //    var soldier = Instantiate(FlagZombie, basePostion, new Quaternion(0f, 1f, -0.3f, 0), LeftPlayer.transform.GetChild(1).transform);
        //    soldier.GetComponent<SoldierManager>().Init(XMLTools.GetSoldierByType(SoldierType.FlagZombie));
        //    soldier.name += Time.time;
        //}
        for (int i = 0; i < 1; i++)
        {
            var basePostion = RightPlayer.transform.GetChild(0).position;
            var random = Random.insideUnitCircle * 5;
            basePostion += new Vector3(-Mathf.Abs(random.x), random.y);
            var soldier = Instantiate(FlagZombie, basePostion, new Quaternion(0.3f, 0f, 0f, 1f), RightPlayer.transform.GetChild(1).transform);
            soldier.GetComponent<SoldierManager>().Init(XMLTools.GetSoldierByType(SoldierType.FlagZombie));
            soldier.name += Time.time;
        }
    }

    public GameObject LeftPlayer,RightPlayer;

    public Soldier LeftHome, RightHome;
    public Image RightHealthy, LeftHealthy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RightHealthy.transform.localScale = new Vector3(RightHome.Healthy/100f, 1, 1);
        LeftHealthy.transform.localScale = new Vector3(LeftHome.Healthy / 100f, 1, 1);
    }
}
