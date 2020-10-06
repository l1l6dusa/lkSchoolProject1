using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thief : MonoBehaviour
{
    [SerializeField] private int _speed;
    private Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rbody.MovePosition(gameObject.transform.position + Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rbody.MovePosition(gameObject.transform.position + Vector3.right * _speed * Time.deltaTime);
        }
    }
}
