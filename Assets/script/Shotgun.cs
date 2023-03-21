using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public int bulletNum = 3;//子弹数量

    public int bulletAngle = 15;//子弹间隔角度
    // Start is called before the first frame update

    protected override void Fire()//射击不同，所以覆写射击函数
    {
        int median = bulletNum / 2;

        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzllePos.position;//枪口位置创建子弹

            if (bulletNum % 2 == 1)
            {
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * direction);
            }
            else
            {
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle*(i-median)+bulletAngle/2, Vector3.forward) * direction);
            }
        }
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position=shellPos.position;
        shell.transform.rotation=shellPos.rotation;
    }

}
