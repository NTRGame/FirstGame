using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public GameObject A, B;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        A.transform.position = A.transform.position - new Vector3(Time.deltaTime, 0, 0);
        B.transform.position = B.transform.position + new Vector3(Time.deltaTime, 0, 0);
        if (A.GetComponent<BoxCollider>().isTrigger)
        {
            Debug.Log("yes");
        }
    }
}
