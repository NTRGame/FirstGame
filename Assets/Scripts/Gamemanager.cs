using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public void CreateSoldier()
    {
        GameObject FlagZombie = Resources.Load("Prefabs/FlagZombie", typeof(GameObject)) as GameObject;
        for(int i = 0; i < 1; i++)
        {
            var basePostion = LeftPlayer.transform.GetChild(0).position;
            var random = Random.insideUnitCircle*5;
            basePostion += new Vector3(Mathf.Abs(random.x), random.y);
            var soldier = Instantiate(FlagZombie, basePostion, new Quaternion(0f, 1f, -0.3f, 0), LeftPlayer.transform.GetChild(1).transform);
            soldier.GetComponent<SoldierManager>().Init(XMLTools.GetSoldierByType(SoldierType.FlagZombie, PlayerType.Left));
            soldier.name += Time.time;
        }
        for (int i = 0; i < 1; i++)
        {
            var basePostion = RightPlayer.transform.GetChild(0).position;
            var random = Random.insideUnitCircle * 5;
            basePostion += new Vector3(-Mathf.Abs(random.x), random.y);
            var soldier = Instantiate(FlagZombie, basePostion, new Quaternion(0.3f, 0f, 0f, 1f), LeftPlayer.transform.GetChild(1).transform);
            soldier.GetComponent<SoldierManager>().Init(XMLTools.GetSoldierByType(SoldierType.FlagZombie, PlayerType.Right));
            soldier.name += Time.time;
        }
    }

    public GameObject LeftPlayer,RightPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
