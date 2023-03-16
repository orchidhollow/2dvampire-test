using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform PlayerTransform;
    private Transform SelfTransform;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        SelfTransform=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var SelfPositon=SelfTransform.position;
        var PlayerPositon=PlayerTransform.position;
        var Deriction = (PlayerPositon - SelfPositon).normalized;

        rb.velocity = Deriction * speed;

    }
}
