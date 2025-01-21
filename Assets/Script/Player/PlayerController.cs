using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputControl inputControl;
    private PhysicsCheck physicsCheck;
    private GameManager gameManager;
    private Vector2 inputDirection;
    [Header("基础属性")]
    public float moveSpeed;
    public float jumpForce;

    [Header("bool")] 
    [SerializeField]private bool isBubble;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        gameManager = FindObjectOfType<GameManager>();
        
        
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Bubble.performed += BubbleStart;
        inputControl.GamePlay.Bubble.canceled += BubbleEnd;

    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        BlowBubbles();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);
        var faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = -1;
        if (inputDirection.x < 0)
            faceDir = 1;
        transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
    }
    
    //TODO:跳跃优化
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
    
    //TODO:思路1（长按让泡泡变大）  
    //下面这个就是吹泡泡，按下开始吹，松开就结束，可以设计泡泡爆炸或者什么
    private void BubbleStart(InputAction.CallbackContext obj)
    {
        Debug.Log("BubbleStart");
        isBubble = true;
    }
    
    private void BubbleEnd(InputAction.CallbackContext obj)
    {
        Debug.Log("BubbleEnd");
        isBubble = false;
        gameManager.FireBubble();
    }

    private void BlowBubbles()
    {
        if (isBubble)
        {
            Debug.Log("Bubbles");
            
        }
        //调用gameManager里面发射泡泡的函数
        
    }
    
    
    
    
    
}
