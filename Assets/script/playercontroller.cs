using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float speed=10;//角色移动速度
    private Transform SelfTransform;

    public Image HP_image;//血条
    public Image HP_fade;//虚血条
    public float HP=100f, HPfade=100f;     //血量和延迟生命值
    public float fadeTimer = 0;  //血量延迟计时器 
    public float fadeTime = 1f;  //血量扣除延迟时间
    Coroutine damage_Coroutione= null;  //持续扣血协程对象

    public GameObject[] guns;
    private int gunNum;


    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SelfTransform=GetComponent<Transform>();
        guns[0].SetActive(true);             //激活第一个枪作为初始
    }

    // Update is called once per frame
    void Update()
    {
        move();
        switchGun();
        //HP_Change();
        fadeTimer += Time.deltaTime;      //延时伤害计时

        if (HPfade >= HP && fadeTimer > fadeTime)  //延时显示虚血生命值扣除动画
        {
            HPfade = Mathf.Lerp(HPfade, HP, Time.deltaTime * 4);
        }
        HP_image.fillAmount = HP / 100f;
        HP_fade.fillAmount = HPfade / 100f;

    }
    void move()
    {
        float HorizontalMove = 0;
        float VerticalMove = 0;
        float FaceDirection = 0;
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");
        FaceDirection = Input.GetAxisRaw("Horizontal");
        if(HorizontalMove!=0||VerticalMove!=0)
        {
            rb.velocity = new Vector2(HorizontalMove*speed, VerticalMove*speed);
            animator.SetFloat("isRunning", Mathf.Abs(HorizontalMove+VerticalMove));
            if(FaceDirection!=0)
            {
                transform.localScale = new Vector3(FaceDirection, 1, 1);
            }
        }
        
    }
    //void HP_Change()
    //{
    //    if (Input.GetMouseButtonDown(0))//按下鼠标左键时
    //    {
    //        fadeTimer = 0;             //重置计时器
    //        Damage_Once(10);           //单次扣除生命值
    //    }
    //    else if (Input.GetMouseButtonDown(1))//按下鼠标右键
    //    {
    //        if (damage_Coroutione != null)     //如果已经受伤，则刷新持续时间
    //        {
    //            StopCoroutine(damage_Coroutione);
    //            damage_Coroutione = StartCoroutine(Damage_Over_Time(5, 2));
    //        }
    //        else
    //        {
    //            damage_Coroutione = StartCoroutine(Damage_Over_Time(5, 2));
    //        }
    //    }

    //    fadeTimer += Time.deltaTime;      //延时伤害计时

    //    if (HPfade >= HP && fadeTimer > fadeTime)  //延时显示虚血生命值扣除动画
    //    {
    //        HPfade = Mathf.Lerp(HPfade, HP, Time.deltaTime * 4);
    //    }
    //}

    void Damage_Once(float damage) //单体伤害，参数为单次伤害量
    {
        HP-=damage;
    }

    public IEnumerator Damage_Over_Time(float damage,int dutation)//持续伤害，每秒伤害量，持续时间
    {
        float timer = 0;//计时变量
        while(HP>=0&&timer<=dutation)//生命值大于0且持续时间未结束
        {
            HP-=damage*Time.deltaTime;//扣除生命值
            timer += Time.deltaTime;//增加计时时间
            yield return null;     //等待一帧
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            fadeTimer = 0;
            Damage_Once(10);
            var Hurt = SelfTransform.position - collision.transform.position;
            rb.velocity = Hurt.normalized * 10;
        }

    }

    void switchGun() 
    {
        if(Input.GetKeyDown(KeyCode.Q))//按Q
        {
            guns[gunNum].SetActive(false);//取消激活当前枪械
            if(--gunNum<0)//枪械下标减一
            {
                gunNum=guns.Length-1;
            }
            guns[gunNum].SetActive(true);//激活当前下标的枪
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            guns[gunNum].SetActive(false);
            if(++gunNum>guns.Length-1) 
            {
                gunNum=0;
            }
            guns[gunNum].SetActive(true);
        }
    }

}
