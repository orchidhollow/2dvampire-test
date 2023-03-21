using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShell : MonoBehaviour
{
    public float speed;//�����׳��ٶ�

    public float stopTime = 0.5f;//ͣ��ʱ��

    public float fadeSpeed = 0.1f;//��ʧʱ��

    private Rigidbody2D rb;

    private SpriteRenderer sprite;//������Ⱦ��
    // Start is called before the first frame update
    void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        sprite=GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    private void OnEnable()//���屻����ʱ����
    {
        rb.velocity = Vector2.up * speed;//���ϵ��ٶ�

        sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b,1);//����������ɫ
        rb.gravityScale= 3;
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(stopTime);
        rb.velocity=Vector2.zero;
        rb.gravityScale=0;

        while(sprite.color.b>0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b - fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
