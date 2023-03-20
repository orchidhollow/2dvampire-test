using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;//ËÙ¶È

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
 

    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction*speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag=="enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().EnemyMove(true,speed,collision.transform.position+(collision.transform.position-transform.position)*5);
        }
        Destroy(gameObject);
    }
}
