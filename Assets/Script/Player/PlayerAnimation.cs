using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    private Animator anim;
    private Rigidbody2D rb;
    private void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        //Mathf.Abs取绝对值
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        // anim.SetFloat("velocityY", rb.velocity.y);
        //后续做跳跃动画可以延续这个写
       
    }
    
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
        //这个同理
    }
    
}
