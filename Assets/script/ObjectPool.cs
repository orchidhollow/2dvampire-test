using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool //������أ����Բ���Ҫ�̳�MonoBehaviour
{

    private static ObjectPool instance;//����һ����̬ʵ��

    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();//����һ���ֵ䣬�ַ�����Ϊkey������Ϊvalue��ʵ��������֮���ã�

    private GameObject pool;//������������ĸ�����
    
    public static ObjectPool Instance//getʵ�������û��������һ������ֵ
    { 
        get { 
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance; 
        }
    }

    public GameObject GetObject(GameObject prefab)//��ȡgameobject
    {
        GameObject _object;//���ڻ�ȡ��������
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0) //ʹ��Ԥ��������ֲ�ѯ�Ƿ���ڴ洢��Ԥ����ĳأ����˶Գ���������
        {
            _object = GameObject.Instantiate(prefab);//���û����ʵ����һ��������
            PushObject(_object);//ʹ��PushObject�����������
            if (!pool) //�Ƿ���ڶ���ظ�����
            {
                pool = new GameObject("ObjectPool");//��������ھʹ���һ����������ΪOjectPool
            }
            GameObject childPool = GameObject.Find(prefab.name + "Pool");//���ҳ������Ƿ�����Ӷ���صĸ�����
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");//�����������ʹ��Ԥ��������ִ���������
                childPool.transform.SetParent(pool.transform);//����Ϊ��������������
            }
            _object.transform.SetParent(childPool.transform);//Ȼ�󽫸����ɵ�������Ϊ��Ӧ�Ӷ�������������
        }
        _object = objectPool[prefab.name].Dequeue();//��Ԥ��������ȡ��������ڵ�һ������
        _object.SetActive(true);//�����������
        return _object;//���ظ�������ʹ��
    }

    public void PushObject(GameObject prefab) //���������Żس���
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);//ʹ��replace������(Clone)�滻Ϊ��
        if (!objectPool.ContainsKey(_name)) //�û�ȡ�������Ʋ����Ƿ���ڶ�Ӧ�Ķ����
        {
            objectPool.Add(_name, new Queue<GameObject>());//���û����������һ��
        }
        objectPool[_name].Enqueue(prefab);//��������������
        prefab.SetActive(false);//��ȡ������
    }

}
