using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [Header("基础属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("无敌")]
    public float invulunerableDuration;
    private float invulunerableCounter;
    public bool invulnerable;
    
    
    public UnityEvent<Character> OnHealthChange;  //用来更新ui
    public UnityEvent<Transform> OnTakeDamage;   //用来实现受伤时的状态
    public UnityEvent OnDie;                    //死
    private void Awake()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulunerableCounter -= Time.deltaTime;
            if(invulunerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
       
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        if (currentHealth - attacker.damage > 0)  
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulunerableCounter = invulunerableDuration;
        }
    }
}

   
