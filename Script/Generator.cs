using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //定义游戏拍对象
    public GameObject plat;
    //定义父节点
    public Transform parrent;
    //定义奖杯位置
    public Transform pole;
    //定义江北最高位置
    public Transform finishLine;

    public float rotaedMin;
    public float rotaedMax;
    //定义拍之间的高度
    public float offSet;

    //定义开始高度
    public float startHight;
    //定义开始角度
    public float startRotation;
    private int size;

    public int diamondChange;
    void Start()
    {
        size = FindObjectOfType<GameManager>().totalHeight;
        MakeLevel();
    }
    void MakeLevel()
    {
        //赋值起始高度角度
        float hight = startHight;
        float rot = startRotation;

        bool lastHadDiamond = false;

        for (int i = 0; i < size; i++)
        {
            //实例化网球拍，设置位置跟角度
            GameObject newPlat = Instantiate(plat);
            newPlat.transform.position = Vector3.up * hight;
            newPlat.transform.Rotate(Vector3.up * rot);

            //设置父节点
            newPlat.transform.SetParent(parrent, false);

            //设置钻石拍子垫上的钻石生成
            bool diamond = i > 0 && !lastHadDiamond && Random.Range(0, diamondChange) == 0;
            newPlat.GetComponent<Playtform>().SetDiamond(diamond, canHaveBouncePad: i < size - 3 && i > 0);

            //设置随机生成范围
            float rotationRadom = Random.Range(rotaedMin, rotaedMax);
            //设置边缘第1个拍的的范围
            if (i == 0)
                rotationRadom = Random.Range((rotaedMin + rotaedMax) / 2, rotaedMax);
            //设置拍子的左右生成
            bool ratateLeft = Random.Range(0, 2) == 0;

            rot += ratateLeft ? -rotationRadom : rotationRadom;
            hight += offSet;

        }
        //设置奖杯最高处
        finishLine.position = Vector3.up * hight;
        finishLine.SetParent(parrent, false);

        Vector3 poleScale = pole.localScale;
        pole.localScale = new Vector3(poleScale.x, hight, poleScale.z);


    }
}
