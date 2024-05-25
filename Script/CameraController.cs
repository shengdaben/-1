using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public float offset;
    public float moveSpeed;
    private Vector3 target;
   
    void Update()
    {
        //不断地移动target点位达到玩家的最高位置
        if (player.transform.position.y > target.y +offset)
        {
            target = Vector3.up * (player.transform.position.y - offset);
        }

        //移动相机朝最高点
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
    }
}
