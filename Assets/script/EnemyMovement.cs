using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform PlayerTransform;
    private Transform SelfTransform;
    public float speed = 5f;
    public float back = 5f;
    public bool  is_back=false;
    Coroutine coroutine=null;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        SelfTransform=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var SelfPositon = SelfTransform.position;
        var PlayerPositon = PlayerTransform.position;
        var Deriction = (PlayerPositon - SelfPositon).normalized;
        if (timer >= 0.1)
        {
            EnemyMove(is_back, speed, Deriction);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    
    public void EnemyMove(bool is_back,float speed,Vector3 Deriction)
    {
        if (is_back)
        {
            coroutine = StartCoroutine(Force(Deriction));            
        }
        else
        {
            rb.velocity = Deriction * speed;
        }
    }

    public IEnumerator Force(Vector3 deriction)
    {
        rb.AddForce(deriction, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        is_back=false;
    }
    }
