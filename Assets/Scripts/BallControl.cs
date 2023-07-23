using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BallControl : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Vector2 startPosition = new Vector2(0,0);
    Vector2 endPosition= new Vector2(0,0);
    [SerializeField] float moveSpeed=10f;
    [SerializeField] bool hasClicked=false;
    [SerializeField] bool isDragging=false;
    private void Awake()
    {
        myRigidbody= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      if(Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
      if(Input.GetMouseButton(0) && hasClicked )
        {
            MouseDrag();
        }
      if(Input.GetMouseButtonUp(0) && isDragging) 
        {
            MouseUp();
        }
    }

     void MouseUp()
    {
        float ballVelocityX = (startPosition.x - endPosition.x);
        float ballVelocityY = (startPosition.y - endPosition.y);
        Vector2 velocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        myRigidbody.velocity=velocity*moveSpeed;
        isDragging = false;
        hasClicked = false;
    }

    void MouseDrag()
    {
        endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void MouseDown()
    {
        startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hasClicked = true;
    }

     void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag =="Ground")
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }
}
