using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    #region
    /// <summary>
    /// 单例模式
    /// </summary>
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
                //则创建一个
                _instance = GameObject.Find("Map").GetComponent<MapManager>();
            //返回这个实例
            return _instance;
        }
    }
    #endregion

    /// <summary>
    /// 士兵生成时候调用
    /// </summary>
    /// <param name="soldierObject">士兵的GameObject</param>
    public void InitSoldier(GameObject soldierObject)
    {

    }

    /// <summary>
    /// 士兵死亡时候调用
    /// </summary>
    /// <param name="soldierObject">士兵的GameObject</param>
    public void RemoveSoldier(GameObject soldierObject)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
