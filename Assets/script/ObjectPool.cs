using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool //无需挂载，所以不需要继承MonoBehaviour
{

    private static ObjectPool instance;//声明一个静态实例

    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();//生成一个字典，字符串作为key，队列为value，实例化留作之后用；

    private GameObject pool;//所有生成物体的父物体
    
    public static ObjectPool Instance//get实例，如果没有则生成一个并赋值
    { 
        get { 
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance; 
        }
    }

    public GameObject GetObject(GameObject prefab)//获取gameobject
    {
        GameObject _object;//用于获取池中物体
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0) //使用预制体的名字查询是否存在存储该预制体的池，并核对池中物体数
        {
            _object = GameObject.Instantiate(prefab);//如果没有则实例化一个新物体
            PushObject(_object);//使用PushObject函数放入池中
            if (!pool) //是否存在对象池父物体
            {
                pool = new GameObject("ObjectPool");//如果不存在就创建一个物体起名为OjectPool
            }
            GameObject childPool = GameObject.Find(prefab.name + "Pool");//查找场景中是否存在子对象池的父物体
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");//如果不存在则使用预制体的名字创建新物体
                childPool.transform.SetParent(pool.transform);//并设为对象池体的子物体
            }
            _object.transform.SetParent(childPool.transform);//然后将刚生成的物体设为对应子对象池体的子物体
        }
        _object = objectPool[prefab.name].Dequeue();//按预制体名获取到对象池内的一个物体
        _object.SetActive(true);//并激活该物体
        return _object;//返回给调用者使用
    }

    public void PushObject(GameObject prefab) //用完的物体放回池中
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);//使用replace函数将(Clone)替换为空
        if (!objectPool.ContainsKey(_name)) //用获取到的名称查找是否存在对应的对象池
        {
            objectPool.Add(_name, new Queue<GameObject>());//如果没有则新生成一个
        }
        objectPool[_name].Enqueue(prefab);//将该物体放入池中
        prefab.SetActive(false);//并取消激活
    }

}
