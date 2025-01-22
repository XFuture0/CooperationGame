using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("基础属性")]
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundLayer;
    [Header("bool")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;
    
    private void Awake()
    {
        
    }
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        //check ground
        isGround =  Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRadius,groundLayer);
        //check wall
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}