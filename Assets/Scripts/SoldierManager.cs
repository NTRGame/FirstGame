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
    protected List<GameObject> CLoseTarget;
    [SerializeField]
    protected List<GameObject> RemoteTarget;
    public bool IsInit = false;

    public AnimatorOverrideController[] tankatate;

    public IEnumerator Death()
    {
        IsMove = false;
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<BoxCollider>());
        GetComponent<Animator>().SetTrigger("Death");

        yield return null;
    }

    public void Init(Soldier soldier)
    {
        this.soldier = soldier;
        IsInit = true;
        GetComponent<SphereCollider>().radius = soldier.AttackDistance;
        if (soldier.soldierType != SoldierType.Home)
        {
            GetComponent<Animator>().runtimeAnimatorController = tankatate[(int)soldier.soldierType];
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (IsInit && other.gameObject.GetComponent<SoldierManager>().IsInit &&  other.gameObject.GetComponent<SoldierManager>().soldier != null && other.gameObject.GetComponent<SoldierManager>().playerType != playerType)
        {
            CLoseTarget.Add(other.gameObject);
        }
    }

    public void OnCollisionExit(Collision other)
    {
        CLoseTarget.Remove(other.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetType().Equals(typeof(SphereCollider)))
        {
            return;
        }
        try
        {
            if (other.gameObject.GetComponent<SoldierManager>().soldier != null && other.gameObject.GetComponent<SoldierManager>().playerType != playerType)
            {
                RemoteTarget.Add(other.gameObject);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }     
    }

    public void OnTriggerExit(Collider other)
    {
        RemoteTarget.Remove(other.gameObject);
    }

    IEnumerator RemoteAttack()
    {
        float time = soldier.Frequency;
        while (true)
        {
            time -= Time.deltaTime;
            while (RemoteTarget.Count > 0 && (RemoteTarget[0] == null || !RemoteTarget[0].activeSelf))
            {
                RemoteTarget.RemoveAt(0);
            }
            if (time <= 0)
            {
                if (RemoteTarget.Count > 0 && RemoteTarget[0].activeSelf)
                {
                    GameObject Bullet = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
                    var bullet = Instantiate(Bullet, transform.position, new Quaternion(0, 0, 0, 0), transform);
                    bullet.GetComponent<BulletManager>().Init(RemoteTarget[0]);
                    bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Bullet/" + soldier.bulletType, typeof(Sprite)) as Sprite;
                    soldier.Attack(RemoteTarget[0].GetComponent<SoldierManager>().soldier);
                    //如果死亡
                    if (RemoteTarget[0].GetComponent<SoldierManager>().soldier.Healthy <= 0)
                    {
                        if (RemoteTarget[0].GetComponent<SoldierManager>().soldier.IsAlive)
                        {
                            RemoteTarget[0].GetComponent<SoldierManager>().soldier.IsAlive = false;
                            StartCoroutine(RemoteTarget[0].GetComponent<SoldierManager>().Death());
                        }
                    }
                }

                time = soldier.Frequency;
                //重置攻击cd

            }
            yield return null;
        }
    }

    IEnumerator CloseAttack()
    {
        float time = soldier.Frequency;
        while (true)
        {
            time -= Time.deltaTime;
            while (CLoseTarget.Count > 0 && (CLoseTarget[0] == null || !CLoseTarget[0].activeSelf))
            {
                CLoseTarget.RemoveAt(0);
            }
            if (time <= 0)
            {
                if(CLoseTarget.Count >0 && CLoseTarget[0].activeSelf)
                {
                    
                    soldier.Attack(CLoseTarget[0].GetComponent<SoldierManager>().soldier);
                    //如果死亡
                    if (CLoseTarget[0].GetComponent<SoldierManager>().soldier.Healthy <= 0)
                    {
                        if (CLoseTarget[0].GetComponent<SoldierManager>().soldier.IsAlive)
                        {
                            CLoseTarget[0].GetComponent<SoldierManager>().soldier.IsAlive = false;
                            StartCoroutine(CLoseTarget[0].GetComponent<SoldierManager>().Death());                            
                        }
                    }
                }
                
                time = soldier.Frequency;
                //重置攻击cd

            }
            yield return null;
        }
    }

    void Awake()
    {
        CLoseTarget = new List<GameObject>();
        RemoteTarget = new List<GameObject>();
    }

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3();
        startTime = Time.time;
        startPostion = transform.position;
        if (name.Equals("Home")){
            Init( XMLTools.GetSoldierByType(SoldierType.Home));
            if(playerType == PlayerType.Left)
            {
                GameManager.Instance.LeftHome = soldier;
            }
            else
            {
                GameManager.Instance.RightHome = soldier;
            }
            StartCoroutine(CloseAttack());
            StartCoroutine(RemoteAttack());
            return;
        }
        StartCoroutine(CloseAttack());
        StartCoroutine(RemoteAttack());
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
        if(CLoseTarget.Count >0 || RemoteTarget.Count > 0)
        {
            IsMove = false;
            GetComponent<Animator>().SetBool("IsMove", IsMove);
        }
        else
        {
            IsMove = true;
            GetComponent<Animator>().SetBool("IsMove", IsMove);
        }
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
