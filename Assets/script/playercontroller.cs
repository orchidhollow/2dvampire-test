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
    public float speed=10;//��ɫ�ƶ��ٶ�
    private Transform SelfTransform;

    public Image HP_image;//Ѫ��
    public Image HP_fade;//��Ѫ��
    public float HP=100f, HPfade=100f;     //Ѫ�����ӳ�����ֵ
    public float fadeTimer = 0;  //Ѫ���ӳټ�ʱ�� 
    public float fadeTime = 1f;  //Ѫ���۳��ӳ�ʱ��
    Coroutine damage_Coroutione= null;  //������ѪЭ�̶���

    public GameObject[] guns;
    private int gunNum;


    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SelfTransform=GetComponent<Transform>();
        guns[0].SetActive(true);             //�����һ��ǹ��Ϊ��ʼ
    }

    // Update is called once per frame
    void Update()
    {
        move();
        switchGun();
        //HP_Change();
        fadeTimer += Time.deltaTime;      //��ʱ�˺���ʱ

        if (HPfade >= HP && fadeTimer > fadeTime)  //��ʱ��ʾ��Ѫ����ֵ�۳�����
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
    //    if (Input.GetMouseButtonDown(0))//����������ʱ
    //    {
    //        fadeTimer = 0;             //���ü�ʱ��
    //        Damage_Once(10);           //���ο۳�����ֵ
    //    }
    //    else if (Input.GetMouseButtonDown(1))//��������Ҽ�
    //    {
    //        if (damage_Coroutione != null)     //����Ѿ����ˣ���ˢ�³���ʱ��
    //        {
    //            StopCoroutine(damage_Coroutione);
    //            damage_Coroutione = StartCoroutine(Damage_Over_Time(5, 2));
    //        }
    //        else
    //        {
    //            damage_Coroutione = StartCoroutine(Damage_Over_Time(5, 2));
    //        }
    //    }

    //    fadeTimer += Time.deltaTime;      //��ʱ�˺���ʱ

    //    if (HPfade >= HP && fadeTimer > fadeTime)  //��ʱ��ʾ��Ѫ����ֵ�۳�����
    //    {
    //        HPfade = Mathf.Lerp(HPfade, HP, Time.deltaTime * 4);
    //    }
    //}

    void Damage_Once(float damage) //�����˺�������Ϊ�����˺���
    {
        HP-=damage;
    }

    public IEnumerator Damage_Over_Time(float damage,int dutation)//�����˺���ÿ���˺���������ʱ��
    {
        float timer = 0;//��ʱ����
        while(HP>=0&&timer<=dutation)//����ֵ����0�ҳ���ʱ��δ����
        {
            HP-=damage*Time.deltaTime;//�۳�����ֵ
            timer += Time.deltaTime;//���Ӽ�ʱʱ��
            yield return null;     //�ȴ�һ֡
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
        if(Input.GetKeyDown(KeyCode.Q))//��Q
        {
            guns[gunNum].SetActive(false);//ȡ�����ǰǹе
            if(--gunNum<0)//ǹе�±��һ
            {
                gunNum=guns.Length-1;
            }
            guns[gunNum].SetActive(true);//���ǰ�±��ǹ
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
