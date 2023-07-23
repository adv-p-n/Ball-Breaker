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
    [SerializeField] int ballCount = 1;
    [SerializeField] bool hasClicked=false;
    [SerializeField] bool isDragging=false;
    [SerializeField] bool canInteract = true;
    LineRenderer arrow;

    private void Awake()
    {
        myRigidbody= GetComponent<Rigidbody2D>();
        arrow= GetComponentInChildren<LineRenderer>();

    }

    void Update()
    {
      if(Input.GetMouseButtonDown(0) && canInteract)
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
        arrow.enabled = false;
        float ballVelocityX = (startPosition.x - endPosition.x);
        float ballVelocityY = (startPosition.y - endPosition.y);
        Vector2 velocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        myRigidbody.velocity=velocity*moveSpeed;
        if (myRigidbody.velocity == Vector2.zero) { return; }
        isDragging = false;
        hasClicked = false;
        canInteract= false;
    }

    void MouseDrag()
    {
        arrow.enabled = true;
        endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diffX= startPosition.x - tempPos.x;
        float diffY= startPosition.y - tempPos.y;
        if (diffY <= 0 ) { diffY = 0.1f; }
        float theta =Mathf.Rad2Deg * MathF.Atan(diffX/diffY);
        arrow.transform.rotation = Quaternion.Euler(0,0,-theta);
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
            canInteract= true;
        }
    }
}
