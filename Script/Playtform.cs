using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playtform : MonoBehaviour
{
    //回弹的动画
    public Animator bounceAnim;
    //圆环的动画
    public Animator circle;
    public Image circleImage;

    //生成粒子
    public ParticleSystem shellParticles;
    public Renderer[] renderers;
    //弹力垫
    public GameObject bouncePad;
    //跳跃板子的生成概率
    public int bouncePadChance;
    //设置弹力垫的声音
    public AudioSource bouncePadSource;


    //钻石声音
    public AudioSource diamondSound;
    public GameObject diamondHolder;
    //钻石的特效
    public ParticleSystem diamondParticles;
    //判断牌子上有没有钻石
    private bool diamond;
    [HideInInspector]
    public bool hasBouncePad;

    private GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }


    //获取班子的材质和粒子特效,当玩家装机板子给个回弹
    public void Bounce(Vector3 particlePostion,Material mat)
    {
        //拿到名字触发回弹动画
        bounceAnim.SetTrigger("Bounce");
        circle.SetTrigger("Play");

        if (hasBouncePad)
        {
            bouncePadSource.Play();
        }

        //触发粒子特效
        shellParticles.transform.position = particlePostion;
        shellParticles.Play();

        if (diamond)
        {
            PickupDiamond();
            diamond = false;
        }

        //改变牌子颜色
        SetMaterials(mat);
    }

    //改变材质颜色
    void SetMaterials(Material mat)
    {
        foreach (Renderer rend in renderers)
        {
            rend.material = mat;
        }
    }

    //拾取砖石和播放声音
    void PickupDiamond()
    {
        manager.AddDiamonds(1);
        diamondSound.Play();
        diamondHolder.SetActive(false);
        diamondParticles.Play();
    }

    //设置钻石位
    public void SetDiamond(bool hasDiamond,bool canHaveBouncePad)
    {
        
        diamond = hasDiamond;
        //设置钻石范围
        bouncePad.SetActive(false);
        if (!hasDiamond)
        {
            diamondHolder.SetActive(false);
            if (Random.Range(0,bouncePadChance) == 0 && canHaveBouncePad)
            {
                hasBouncePad = true;
                bouncePad.SetActive(true);
                circleImage.color = Color.red;
            }
        }

    }
}
