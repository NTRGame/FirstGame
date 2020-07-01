using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    [SerializeField]
    private Soldier soldier;
    private bool IsMove = true;

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
                StartCoroutine(Attack());
            }
        }
        catch
        {

        }     
    }

    IEnumerator Attack()
    {
        float time = soldier.Frequency;
        while(target != null && target.activeSelf)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                soldier.Attack(target.GetComponent<SoldierManager>().soldier);
                if (target.GetComponent<SoldierManager>().soldier.Healthy <= 0 )
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
            yield return null;
        }
    }

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3();
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
                transform.position = transform.position + new Vector3(Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position = transform.position - new Vector3(Time.deltaTime, 0, 0);
            }
        }
    }
}
