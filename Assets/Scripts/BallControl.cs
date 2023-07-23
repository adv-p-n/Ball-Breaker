using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidbody;
    Vector2 position = new Vector2(0,0);
    Vector2 moveInput= new Vector2(0,0);
    [SerializeField] float moveSpeed=0.5f;
    float xRange=2.6f;
    float yRange = 4f;
    [SerializeField] bool isDragging=false;
    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody= GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        position= transform.position;
        Debug.Log(position);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
        BallPhysics();
    }

    private void MoveBall()
    {
        position.x = Mathf.Clamp(position.x +( moveInput.x*moveSpeed), -xRange, xRange);
        position.y = Mathf.Clamp(position.y +( moveInput.y*moveSpeed), -yRange, yRange);
        transform.position=new Vector3(position.x,position.y,0);
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(position);
    }
     void BallPhysics()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mySpriteRenderer.color = Color.red;
            isDragging = true;
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            mySpriteRenderer.color = Color.white;
            isDragging = false;
        }
        if (isDragging) 
        {
            mySpriteRenderer.color = Color.blue;
            transform.position = GetMouseWorldPosition();
            Debug.Log(GetMouseWorldPosition());
        }
        
    }

     Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition ;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
