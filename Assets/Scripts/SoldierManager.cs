using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    [SerializeField]
    public Soldier soldier;
    private bool IsMove = true;

    private GameObject target;

    void OnCollisionEnter(Collision other)
    {
        IsMove = false;       
        GetComponent<Animator>().SetTrigger("Attack");
        target = other.gameObject;
        soldier = new Soldier(10,1,1,SoldierType.FlagZombie,3);
        StartCoroutine(Attack());
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
                if (target.GetComponent<SoldierManager>().soldier.Healthy <= 0 && target.GetComponent<SoldierManager>().soldier.IsAlive)
                {
                    target.GetComponent<SoldierManager>().soldier.IsAlive = false;
                    StartCoroutine(target.GetComponent<SoldierManager>().Death());
                    IsMove = true;
                    GetComponent<Animator>().SetTrigger("Attack");
                }
            }
            yield return null;
        }
    }

    public IEnumerator Death()
    {
        Debug.Log("Death " + name);
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
        if (IsMove)
        {
            if (name.Equals("FlagZombie1"))
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
