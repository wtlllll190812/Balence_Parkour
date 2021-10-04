using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera mainCamera;//主摄像机
    [Header("摄像头调度相关")]
    public float time;//调度所用时间
    public Vector2 targetPos;//目标位置
    public float targetFOV;//目标大小
    [Header("屏幕震动相关")]
    public float shakeTime;//震动时间
    public float shakeLevel = 4f;// 震动幅度
    private float shakeDelta = 0.005f;
    private float currrentVelocityFOV;
    private Vector2 currrentVelocity;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        mainCamera = GetComponent<Camera>();
        targetFOV = mainCamera.orthographicSize;
        targetPos = transform.position;
    }
    private void FixedUpdate()
    {
        FixCamera();
    }
    private void Update()
    {
        ShakeCamera();
    }
    /// <summary>
    /// 调度摄像头
    /// </summary>
    private void FixCamera()
    {
        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, targetFOV, ref currrentVelocityFOV, time);
        Vector3 pos = Vector2.SmoothDamp(transform.position, targetPos, ref currrentVelocity, time);
        pos.z = transform.position.z;
        transform.position = pos;
    }
    public void ShakeCamera()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            mainCamera.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value), shakeDelta * (-1.0f + shakeLevel * Random.value), 1.0f, 1.0f);
        }
    }
}
