using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //定义角色动画
    private Animator jiAnimator;
    public float eggHeight;
    public Material eggMat;

    //跳跃的力
    public float jumpForce;
    public Rigidbody rb;
    //超级跳跃板的力
    public float bonucePadJumpForce;
    public GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        //获取到Player，并且找到动画
        jiAnimator = this.transform.Find("Ji/ji_02").gameObject.GetComponent<Animator>();
    }

    //碰撞器碰撞
    private void OnTriggerEnter(Collider other)
    {
        //碰撞检测处理
        //根据拍子标签和位置判断跳不跳
        if (!other.gameObject.CompareTag("Platform") || transform.position.y < other.gameObject.transform.position.y)
        {
            return;
        }

        //展示平台板特效
        Playtform platform = other.gameObject.transform.parent.GetComponent<Playtform>();
        //设置弹跳;
        platform.Bounce(transform.position - (Vector3.up * eggHeight),eggMat);

        //执行弹跳
        rb.velocity = Vector3.up * (platform.hasBouncePad ? bonucePadJumpForce : jumpForce);
        jiAnimator.Play("Cheer");
        manager.Jump(transform.position);

        //隐藏开始标题，在玩家跳跃之后
        if (transform.position.y > 1f)
        {
            manager.HideTitle();
        }
    }
}
