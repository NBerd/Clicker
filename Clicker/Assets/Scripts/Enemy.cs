using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    public virtual void Hit()
    {
        TakeDamage();
    }

    private void TakeDamage() 
    {
        Debug.Log("Ouch!");
    }
}