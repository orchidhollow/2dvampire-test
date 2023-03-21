using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float interval;//���ʱ��

    public GameObject bulletPrefab;//�ӵ�

    public GameObject shellPrefab;//����

    protected Transform muzllePos;//��ҩλ��

    [SerializeField] protected Transform shellPos;//����λ��

    [SerializeField] protected Vector2 mousePos;//���λ��

    [SerializeField] protected Vector2 direction;//���䷽��

    protected float timer;

    protected float flipY;//ǹеY
    // Start is called before the first frame update
    protected virtual void Start()
    {
        muzllePos = transform.Find("muzzle");
        shellPos = transform.Find("shell");
        flipY = transform.localScale.y;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//��������������ת��Ϊ��������

        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(flipY, -flipY, 1);
        }
        else
        {
            transform.localScale = new Vector3(-flipY, flipY, 1);
        }

        Shoot();
    }

    protected virtual void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;//���λ�ü�ǹеλ��

        transform.right = direction;//ǹе�ľֲ�����ʼ�յ��ڷ��䷽��

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (timer == 0)
            {
                Fire();
                timer = interval;
            }
        }
    }

    protected virtual void Fire()
    {

        //GameObject bullet = Instantiate(bulletPrefab, muzllePos.position, Quaternion.identity) as GameObject ;//�����ӵ�Ԥ����

        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);

        bullet.transform.position = muzllePos.position;

        float angel = Random.Range(-5f, 5f);

        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);//ȡBullet�ű�����SetSpeed,
                                                                                                         //�����z��ƫת���ٳ������������������һ֧�跽��Ϊ��׼��ƫת����

        //Instantiate(shellPrefab, shellPos.position, shellPos.rotation);//λ������Ϊ����λ�ã���ת����Ϊ������ת�Ƕ�

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);

        shell.transform.position = shellPos.position;


    }
}
