using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletType bulletType;
    [SerializeField]
    private bool IsInit = false;
    [SerializeField]
    private GameObject target;
    private float RotateSpeed = 500;
    private float speed = 2;
    private Vector3 startPostion;
    
    public Vector3 endPostion;

    public void Init(GameObject target)
    {
        IsInit = true;
        this.target = target;
    }

    void Start()
    {
        startPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (!IsInit)
            {
                return;
            }
            if (Mathf.Abs(endPostion.sqrMagnitude - transform.position.sqrMagnitude) < 0.2)
            {
                gameObject.SetActive(false);
            }
            if (target == null)
            {
                //if(bulletType!= BulletType.Arrow && bulletType != BulletType.Bullet)
                //    transform.Rotate(Vector3.forward * Time.deltaTime * RotateSpeed);
                var temDirect = endPostion - transform.position;
                var direct = (endPostion - startPostion).magnitude;
                transform.position = transform.position + temDirect * Time.deltaTime * direct / temDirect.magnitude * speed;
            }
            else
            {
                //if (bulletType != BulletType.Arrow)
                //    transform.Rotate(Vector3.forward * Time.deltaTime * RotateSpeed);
                var temDirect = target.transform.position - transform.position;
                var direct = (target.transform.position - startPostion).magnitude;
                transform.position = transform.position + temDirect * Time.deltaTime * direct / temDirect.magnitude * speed;
                endPostion = target.transform.position;
            }
        }
        catch (Exception)
        {
            gameObject.SetActive(false);
        }
        
    }
}
