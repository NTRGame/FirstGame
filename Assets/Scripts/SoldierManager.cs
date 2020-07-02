using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    [SerializeField]
    private bool IsMove = true;
    private float startTime;
    private Vector3 startPostion;
    private float direct;
    public Soldier soldier;
    public PlayerType playerType;
    [SerializeField]
    protected GameObject target;

    public IEnumerator Death()
    {
        Debug.Log("Death " + name);

        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<BoxCollider>());
        GetComponent<Animator>().SetTrigger("Death");

        yield return null;
    }

    public void Init(Soldier soldier)
    {
        this.soldier = soldier;
        GetComponent<SphereCollider>().radius = soldier.AttackDistance;
    }



    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + "---" + other.GetType());
        if (other.GetType().Equals(typeof(SphereCollider)))
        {
            return;
        }
        try
        {
            if (other.gameObject.GetComponent<SoldierManager>().soldier != null && other.gameObject.GetComponent<SoldierManager>().playerType != playerType)
            {
                target = other.gameObject;
                IsMove = false;
                GetComponent<Animator>().SetTrigger("Attack");               
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }     
    }

    IEnumerator Attack()
    {
        float time = soldier.Frequency;
        while (true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                if(target != null && target.activeSelf)
                {
                    soldier.Attack(target.GetComponent<SoldierManager>().soldier);
                    //如果死亡
                    if (target.GetComponent<SoldierManager>().soldier.Healthy <= 0)
                    {
                        IsMove = true;
                        GetComponent<Animator>().SetTrigger("UnAttack");
                        if (target.GetComponent<SoldierManager>().soldier.IsAlive)
                        {
                            target.GetComponent<SoldierManager>().soldier.IsAlive = false;
                            StartCoroutine(target.GetComponent<SoldierManager>().Death());
                        }
                    }
                }
                else
                {
                    if(soldier.soldierType != SoldierType.Home)
                        GetComponent<Animator>().SetTrigger("UnAttack");
                    IsMove = true;
                    target = null;
                }

                //重置攻击cd
                time = soldier.Frequency;
            }
            yield return null;
        }
    }

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3();
        startTime = Time.time;
        startPostion = transform.position;
        if (name.Equals("Home")){
            Init( new Soldier(100f, 0, 5, SoldierType.Home, 9, 0.5f));
            if(playerType == PlayerType.Left)
            {
                GameManager.Instance.LeftHome = soldier;
            }
            else
            {
                GameManager.Instance.RightHome = soldier;
            }
            StartCoroutine(Attack());
            return;
        }
        StartCoroutine(Attack());
        playerType = (PlayerType)Enum.Parse(typeof(PlayerType), transform.parent.name);
    }

    

    public void End()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3();
        if (IsMove && soldier !=null)
        {
            if (playerType == PlayerType.Left)
            {
                //transform.position = Vector3.Lerp(startPostion, GameManager.Instance.RightPlayer.transform.GetChild(0).position, (Time.time-startTime)*0.01f);
                var temDirect = GameManager.Instance.RightPlayer.transform.GetChild(0).position - transform.position;
                direct = (GameManager.Instance.RightPlayer.transform.GetChild(0).position - startPostion).magnitude;
                transform.position = transform.position + temDirect * Time.deltaTime * 0.01f * soldier.Speed * direct / temDirect.magnitude;
            }
            else
            {
                //transform.position = Vector3.Lerp(startPostion, GameManager.Instance.LeftPlayer.transform.GetChild(0).position, (Time.time - startTime) * 0.01f);
                var temDirect = GameManager.Instance.LeftPlayer.transform.GetChild(0).position - transform.position;
                direct = (GameManager.Instance.LeftPlayer.transform.GetChild(0).position - startPostion).magnitude;
                transform.position = transform.position + temDirect * Time.deltaTime * 0.01f * soldier.Speed * direct /temDirect.magnitude;
            }
        }
    }
}
