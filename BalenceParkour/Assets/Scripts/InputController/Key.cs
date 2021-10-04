using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    /// <summary>
    /// 按键名称
    /// </summary>
    private readonly KeyCode keyCode;
    /// <summary>
    /// 按键是否只能按一次
    /// </summary>
    private readonly bool triggerOnce;
    /// <summary>
    /// 缓冲时间
    /// </summary>
    private readonly float waitTime;
    /// <summary>
    /// 计时器
    /// </summary>
    private float timeVal;
    /// <summary>
    /// 按键生效标志,若为缓冲按键，则在调用后记得置否
    /// </summary>
    public bool actionFlag
    {
        set
        {
            timeVal = 0;
        }
        get
        {
            bool flag = Time.time - timeVal < waitTime;
            timeVal = -1;
            return flag;
        }
    }

    /// <summary>
    ///构造函数 
    /// </summary>
    /// <param name="key">按键的编号</param>
    /// <param name="once">长按还是短按</param>
    /// <param name="waitTime">缓冲时间</param>
    public Key(KeyCode key, bool once, float waitTime = 0.01f)
    {
        triggerOnce = once;
        keyCode = key;
        this.waitTime = waitTime;
        timeVal = -10;
    }
    /// <summary>
    /// 按键按下
    /// </summary>
    /// <returns></returns>
    public void CheckKey()
    {
        bool KeyDown = triggerOnce ? Input.GetKeyDown(keyCode) : Input.GetKey(keyCode);
        if (KeyDown && !actionFlag)
            timeVal = Time.time;
    }
    /// <summary>
    /// 检测按键输入
    /// </summary>
    /// <returns></returns>
    public bool GetKey() => Input.GetKey(keyCode);
}
