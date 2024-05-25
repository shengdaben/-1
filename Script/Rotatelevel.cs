using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatelevel : MonoBehaviour
{
    //场景旋转
    //旋转的系数
    public float sensitivity;

    //内部数据
    private Vector2 startPos;
    private float rotation;

    private Vector3 startRotation;

    private void Update()
    {
        //获取当前鼠标位置
        Vector3 mousePos = Input.mousePosition;

        //获取旋转信息
        if (Input.GetMouseButtonDown(0))
        {
            rotation = 0;
            startRotation = transform.eulerAngles;
            startPos = mousePos;
        }
        else if (Input.GetMouseButton(0))
        {
            //计算旋转角根据起始位置
            rotation = startPos.x - mousePos.x;
            Vector3 eulerAngle = startRotation + Vector3.up * rotation * sensitivity;
            transform.rotation = Quaternion.Euler(eulerAngle);
        }
        
    }

}
