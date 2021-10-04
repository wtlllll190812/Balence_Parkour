using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public GameObject easyMap;//简单地图
    public GameObject normalMap;//普通地图
    public GameObject hardMap;//困难地图
    public float moveSpeed;//地图移动速度
    public float mapLength;//地图长度
    public Transform mapDisplayer;//地图显示器
    public int levelNum;//关卡数
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        for (var i = 1; i < 3; i++)
        {
            MapCreate(new Vector3(mapLength * i, 0, 100));
        }
    }
    private void FixedUpdate()
    {
        MapChange();
    }
    private void MapChange()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, mapDisplayer.eulerAngles.z));
        foreach (Transform tileMap in transform)
        {
            if (tileMap.CompareTag("Map"))
            {
                tileMap.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                if (tileMap.transform.position.x < -mapLength * 1.5)
                {
                    MapCreate(new Vector3(tileMap.transform.localPosition.x + mapLength * 3, 0, 100));
                    Destroy(tileMap.gameObject);
                }
            }
        }
    }
    private void MapCreate(Vector3 pos)
    {
        if (levelNum % 2 == 0 && GameState.instance.GameFSM.CurrentStateID == FSMManager.StateID.BalanceGame)
        {
            GameState.instance.AddLeftFarmar(new Vector3(-6, 1.5f, 0), false);
            AudioManager.instance.ClipPlay(2);
        }
        levelNum++;
        GameObject map = Instantiate(MapSelector(), transform.position, transform.rotation);//挑选一张地图
        map.transform.parent = transform;//设置父物体
        map.transform.localPosition = pos;
    }
    /// <summary>
    /// 挑选一个合适的地图
    /// </summary>
    /// <returns>挑选到的地图</returns>
    private GameObject MapSelector()
    {
        GameObject map;
        if (levelNum <= 5 || GameState.instance.GameFSM.CurrentStateID == FSMManager.StateID.NormalGame)
            map = easyMap;
        else if (levelNum % 2 == 1)
            map = normalMap;
        else
        {
            int flag = Random.Range(0, 10);
            if (flag <= 3)
                map = easyMap;
            else if (flag >= 7)
                map = hardMap;
            else
                map = normalMap;
        }
        return map.transform.GetChild(Random.Range(0, map.transform.childCount)).gameObject;
    }
}
