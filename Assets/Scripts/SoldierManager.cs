using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    [SerializeField]
    private Soldier soldier;
    private bool IsMove = true;
    private float startTime;
    private Vector3 startPostion;
    [SerializeField]
    private GameObject target;

    public void Init(Soldier soldier)
    {
        this.soldier = soldier;
        GetComponent<SphereCollider>().radius = soldier.AttackDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.gameObject.GetComponent<SoldierManager>().soldier != null && other.gameObject.GetComponent<SoldierManager>().soldier.playerType != soldier.playerType)
            {
                Debug.Log(other.gameObject.name);
                target = other.gameObject;
                IsMove = false;
                GetComponent<Animator>().SetTrigger("Attack");
                
            }
        }
        catch
        {

        }     
    }

    IEnumerator Attack()
    {
        float time = soldier.Frequency;
        while (true)
        {
            time -= Time.deltaTime;
            if (time <= 0 && target != null && target.activeSelf)
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
        StartCoroutine(Attack());
    }

    public IEnumerator Death()
    {
        Debug.Log("Death " + name);

        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<BoxCollider>());
        GetComponent<Animator>().SetTrigger("Death");
        
        yield return null ;
    }

    public void End()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3();
        if (IsMove && soldier!=null)
        {
            if (soldier.playerType == PlayerType.Left)
            {
                transform.position = Vector3.Lerp(startPostion, GameManager.Instance.RightPlayer.transform.GetChild(0).position, (Time.time-startTime)*0.01f);
            }
            else
            {
                transform.position = Vector3.Lerp(startPostion, GameManager.Instance.LeftPlayer.transform.GetChild(0).position, (Time.time - startTime) * 0.01f);
            }
        }
    }
}
