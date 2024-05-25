using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int totalHeight;
    public Text leftText;
    public Text rightText;

    public GameObject tapToRestart;
    public GameObject tapToContinue;

    public AudioSource jiStart;
    public Text diamondCount;
    public AudioSource jiNiGanMa;

    public Transform player;
    public Transform finishLine;
    bool gameOver;

    private int currentHight;
    private float lastHight;

    public Image backgraoundOverLay;
    public ParticleSystem backgroundPartic;
    private float backgroundAlpha;
    public float backgraoundAlphaSpeed;
    
    public GameObject progressIndicator;
    public ParticleSystem confetti;
    public Image leftCircle;
    public Image rightCircle;
    public GameObject rightBackground;
    public Animator levelDoneText;
    public AudioSource jumpAudio;
    public AudioSource victory;
    
    public GameObject holdToRotate;
    public Animator title;


     bool titleHidden;

    public GameObject plusEffect;

    public Image progressBar;
    
    private void Awake()
    {
        //获取当前等级
        int level = PlayerPrefs.GetInt("level");
        totalHeight = 30 + (level * 5);

        //更新左右两边的等级信息
        leftText.text = (level + 1) + " ";
        rightText.text = (level + 2) + " ";
    }
    private void Start()
    {
        //隐藏UI
        tapToRestart.SetActive(false);
        tapToContinue.SetActive(false);
        jiStart.Play();
    }

    //增加钻石，及时保存，更新钻石数量Ui
    public void AddDiamonds(int count)
    {
        int diamonds = PlayerPrefs.GetInt("Diamonds");
        diamonds += count;

        PlayerPrefs.SetInt("Diamonds", diamonds);
        diamondCount.text = " " + diamonds;
    }

    private void Update()
    {
        //不断检测，游戏是否还在运行
        if (!gameOver)
        {
            if (player.position.y > finishLine.position.y)
            {
                gameOver = true;
                Invoke("DisablePlayer", 1.5f);
                GameOver(true);
            }
            if (player.position.y < lastHight-4.5f)
            {
                gameOver = true;
                jiNiGanMa.Play();
                DisablePlayer();
                GameOver(false);
            }


        }
        else if (Input.GetMouseButtonDown(0))
        {
            //如果游戏结束了，玩家还点击了，那么就重开一局
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //设置跳出的背景色
        if (backgraoundOverLay.color.a < backgroundAlpha)  
        {
            backgraoundOverLay.color = new Color(backgraoundOverLay.color.r, backgraoundOverLay.color.g, backgraoundOverLay.color.b, backgraoundOverLay.color.a + (backgraoundAlphaSpeed * Time.deltaTime));
        }
    }
    void DisablePlayer()
    {
        player.gameObject.SetActive(false);
    }

    //游戏结束函数，参数是否成功
    void GameOver(bool success)
    {
        //隐藏进度条
        progressIndicator.SetActive(false);

        if (success)
        {
            //播放特效，继续下一关
            confetti.Play();

            rightCircle.color = leftCircle.color;
            rightBackground.SetActive(true);

            levelDoneText.SetTrigger("Show");
            victory.Play();

            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            tapToContinue.SetActive(true);

        }
        else
        {
            tapToRestart.SetActive(true);
            holdToRotate.SetActive(false);
        }
    }
    //隐藏标题ui，显示进度Ui
    public void HideTitle()
    {
        if (titleHidden)
        {
            return;
        }
        title.SetTrigger("Hide");
        progressIndicator.SetActive(true);
        holdToRotate.SetActive(true);
    }
    //当玩家跳跃时，执行此函数
    public void Jump(Vector3 playerPos)
    {
        if (playerPos.y < lastHight+0.2f)
        {
            jumpAudio.pitch = 1f;
            jumpAudio.Play();
            return;
        }
        //增长音高，为了奖励效果
        jumpAudio.pitch *= 1.05f;
        jumpAudio.Play();

        currentHight++;
        lastHight = playerPos.y;

        UpdateProgress();

        //更新进度条
        //展示+1特效
        Instantiate(plusEffect, playerPos + Vector3.right * 0.7f, plusEffect.transform.rotation);

    }

   public void UpdateProgress()
    {
        //获取当前进度，并且更新进度
        float percentage = (float)currentHight / (float)totalHeight;

        backgroundAlpha = percentage * 0.7f;
        progressBar.fillAmount = percentage;

        var emission = backgroundPartic.emission;
        emission.rateOverTime = percentage * 4;
    }
}
