using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmarAddition : MonoBehaviour
{
    public Vector3 targetPos;
    // public GameObject prefab;

    public bool isAdd;//是否是增砝码
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.ClipPlay(3);
            if (isAdd)
                GameState.instance.AddLeftFarmar(targetPos, true);
            else
            {
                if (GameState.instance.farmars.Count > 0)
                {
                    GameObject farmar = GameState.instance.farmars[Random.Range(0, GameState.instance.farmars.Count)];
                    GameState.instance.farmars.Remove(farmar);
                    Destroy(farmar);
                }
            }
            Destroy(gameObject);
        }
    }
    // public void AddLeftFarmar(Vector3 target)
    // {
    //     GameObject farmar = Instantiate(prefab, target, Quaternion.identity);
    //     GameState.instance.farmars.Add(farmar);
    // }
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //         AddLeftFarmar(targetPos.position);
    // }
}
