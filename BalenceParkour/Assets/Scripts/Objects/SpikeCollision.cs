using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriterenderer;
    public BoxCollider2D spike;
    // Start is called before the first frame update
    void Start()
    {
        spike = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// 检测是否与玩家碰撞
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Get1");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Get2");
            //受到伤害，游戏结束
            GameState.instance.gameOver = true;
            CameraManager.instance.shakeTime = 0.2f;
            Destroy(other.gameObject);
            AudioManager.instance.ClipPlay(0);
        }
    }
}
