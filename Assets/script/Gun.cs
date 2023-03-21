using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float interval;//间隔时间

    public GameObject bulletPrefab;//子弹

    public GameObject shellPrefab;//弹仓

    protected Transform muzllePos;//弹药位置

    [SerializeField] protected Transform shellPos;//弹仓位置

    [SerializeField] protected Vector2 mousePos;//鼠标位置

    [SerializeField] protected Vector2 direction;//发射方向

    protected float timer;

    protected float flipY;//枪械Y
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//将鼠标的像素坐标转换为世界坐标

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
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;//鼠标位置减枪械位置

        transform.right = direction;//枪械的局部方向始终等于发射方向

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

        //GameObject bullet = Instantiate(bulletPrefab, muzllePos.position, Quaternion.identity) as GameObject ;//生成子弹预制体

        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);

        bullet.transform.position = muzllePos.position;

        float angel = Random.Range(-5f, 5f);

        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);//取Bullet脚本调用SetSpeed,
                                                                                                         //随机以z轴偏转，再乘上正常方向就生成了一支歌方向为基准的偏转方向

        //Instantiate(shellPrefab, shellPos.position, shellPos.rotation);//位置设置为弹仓位置，旋转设置为弹仓旋转角度

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);

        shell.transform.position = shellPos.position;


    }
}
