using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float interval;//���ʱ��

    public GameObject bulletPrefab;//�ӵ�

    public GameObject shellPrefab;//����

    private Transform muzllePos;//��ҩλ��

    [SerializeField] private Transform shellPos;//����λ��

    [SerializeField] private Vector2 mousePos;//���λ��

    [SerializeField] private Vector2 direction;//���䷽��

    private float timer;

    private float flipY;//ǹеY
    // Start is called before the first frame update
    void Start()
    {
        muzllePos = transform.Find("muzzle");
        shellPos = transform.Find("shell");
        flipY=transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��������������ת��Ϊ��������


        Shoot();
    }

    void Shoot()
    {
        direction=(mousePos-new Vector2(transform.position.x, transform.position.y)).normalized;//���λ�ü�ǹеλ��

        transform.right = direction;//ǹе�ľֲ�����ʼ�յ��ڷ��䷽��

        if(timer!=0)
        {
            timer-= Time.deltaTime;
            if(timer<=0)
            {
                timer=0;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(timer==0)
            {
                Fire();
                timer = interval;
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzllePos.position, Quaternion.identity) as GameObject ;//�����ӵ�Ԥ����

        bullet.GetComponent<Bullet>().SetSpeed(direction);//ȡBullet�ű�����SetSpeed

        Instantiate(shellPrefab, shellPos.position, shellPos.rotation);//λ������Ϊ����λ�ã���ת����Ϊ������ת�Ƕ�
    }
}
