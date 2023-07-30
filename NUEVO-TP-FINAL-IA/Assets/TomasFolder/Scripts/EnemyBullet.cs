using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int dmg;
    public float speed;
    public float timeLife;
    float _counterLife;
    Vector3 _dir;
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public Vector3 SetDir
    {
        set
        {
            _dir = value;
        }
    }
    public void Shoot(Vector3 dir)
    {
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    public void Update()
    {
        _rb.velocity = _dir * speed;        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        PlayerModel player = collision.gameObject.GetComponent<PlayerModel>();
    //        player.GetDamage(dmg);
    //    }
    //    Destroy(this.gameObject);
    //}
}
