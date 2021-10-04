using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    /// <summary>
    /// 按键及其对应的动作
    /// </summary>
    public Dictionary<string, Key> ActionDic = new Dictionary<string, Key>();
    /// <summary>
    /// 按键缓冲时间
    /// </summary>
    public float waitTimeJump;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        ActionDic.Add("Jump", new Key(KeyCode.Space, true, waitTimeJump));//添加按键
        ActionDic.Add("Right", new Key(KeyCode.D, false));
        ActionDic.Add("Left", new Key(KeyCode.A, false));
    }
    private void Update()
    {
        foreach (var item in ActionDic)
        {
            item.Value.CheckKey();
        }
    }
    /// <summary>
    /// 获取按键的trigger
    /// </summary>
    /// <param name="ActionName">动作名</param>
    public bool GetTrigger(string ActionName)
    {
        Key key;
        if (ActionDic.ContainsKey(ActionName))
        {
            key = ActionDic[ActionName];
        }
        else
        {
            Debug.LogError("不存在该按键");
            key = null;
        }
        return key.actionFlag;
    }
}
