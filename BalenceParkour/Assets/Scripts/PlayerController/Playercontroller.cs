using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    private Animator playerAnim;
    private Rigidbody2D playerRig;
    SpikeCollision sc;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameOver()
    {
        // if(sc.isSpikeCollision)
        // {
        //     //角色死亡，游戏结束
        // }
    }
}
