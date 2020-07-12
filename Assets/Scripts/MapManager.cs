using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;

    private List<GameObject> soliders;

    private int maxsize = 3;




    //地图长宽是按摄像机视距拉到最大计算的
    private float map_width = 170;

    private float map_height = 20;



    private float ui_map_width;

    private float ui_map_height;




    private List<GameObject> map_point;



    private Vector3 camere_map_scale;

    private Vector3 camere_map_position;

    private Vector3 main_camera_position;

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

        soliders.Add(soldierObject);

    }

    /// <summary>
    /// 士兵死亡时候调用
    /// </summary>
    /// <param name="soldierObject">士兵的GameObject</param>
    public void RemoveSoldier(GameObject soldierObject)
    {

        soliders.RemoveAt(soliders.IndexOf(soldierObject));

        map_point.RemoveAt(0);
    }


    void Awake()
    {
        soliders = new List<GameObject>();

        map_point = new List<GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        ui_map_width = GetComponent<RectTransform>().rect.width;
        ui_map_height = GetComponent<RectTransform>().rect.height;

        camere_map_scale = transform.localScale;
        camere_map_position = transform.position;
        main_camera_position = GameObject.Find("Main Camera").transform.position;
    }

    //将坐标转换为小地图内坐标
    Vector3 changeposition2littlemap(Vector3 normalposition)
    {


        return new Vector3(normalposition.x / map_width * ui_map_width, normalposition.y / map_height * ui_map_height, normalposition.z);
    }

    // Update is called once per frame
    void Update()
    {

        while (map_point.Count < soliders.Count)
        {

            GameObject point = Resources.Load("Prefabs/solider_map", typeof(GameObject)) as GameObject;

            map_point.Add(Instantiate(Resources.Load("Prefabs/solider_map", typeof(GameObject)) as GameObject, Vector3.zero, new Quaternion(0, 0, 0, 0), GameObject.Find("Map").transform));



        }

        for (int i = 0; i < map_point.Count; i++)
        {
            map_point[i].transform.localPosition = changeposition2littlemap(soliders[i].transform.position);

        }

        //GameObject.Find("camera_map").transform.localScale = camere_map_scale * GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize / 15;

        //GameObject.Find("camera_map").transform.position += changeposition2littlemap(GameObject.Find("Main Camera").transform.position - main_camera_position);
        //main_camera_position = GameObject.Find("Main Camera").transform.position;



    }


}
