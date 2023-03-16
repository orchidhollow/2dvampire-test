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
        rb.velocity=Vector2.up*speed;//���ϵ��ٶ�
        StartCoroutine(Stop());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Destroy(gameObject);
    }
}
