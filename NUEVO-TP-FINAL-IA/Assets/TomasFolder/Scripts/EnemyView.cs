using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    Rigidbody _rb;
    //public Animator anim;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    //private void Update()
    //{
    //    var vel = _rb.velocity.magnitude;
    //    anim.SetFloat("Vel", vel);
    //}

    //public void Shoot()
    //{
    //    anim.SetTrigger("Shoot");
    //}
}
