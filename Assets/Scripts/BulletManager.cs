using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private bool IsInit = false;
    [SerializeField]
    private GameObject target;
    private float speed = 500;
    private Vector3 startPostion;
    private Vector3 endPostion;

    public void Init(GameObject target)
    {
        IsInit = true;
        this.target = target;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.Equals(target))
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        startPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInit)
        {
            return;
        }
        if (Mathf.Abs(endPostion.sqrMagnitude - transform.position.sqrMagnitude) < 0.1)
        {
            gameObject.SetActive(false);
        }
        if (target == null)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            var temDirect = endPostion - transform.position;
            var direct = (endPostion - startPostion).magnitude;
            transform.position = transform.position + temDirect * Time.deltaTime * direct / temDirect.magnitude;
        }
        else
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            var temDirect = target.transform.position - transform.position;
            var direct = (target.transform.position - startPostion).magnitude;
            transform.position = transform.position + temDirect * Time.deltaTime * direct / temDirect.magnitude;
            endPostion = target.transform.position;
        }
        
    }
}
