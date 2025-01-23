using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bubble")
        {
            //TODO:感应环的实现
        }
    }
}
