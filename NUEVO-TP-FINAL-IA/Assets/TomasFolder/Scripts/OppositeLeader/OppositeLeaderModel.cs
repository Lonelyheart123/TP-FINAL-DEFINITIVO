using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeLeaderModel : EntityBase, IPoints
{
    public bool isAlive = true;
    //EnemyView anim;
    public float obsRadius;
    public float obsAngle;
    public float multiplier;
    public Action OnCollision = delegate { };
    //public Rigidbody _rb;
    public SecondAgentController agentController;

    public float range;
    public float angle;
    public float maxLife;
    public float _currentLife;
    public float radius;
    public float walkPointRange = 1;
    public int _sense;
    public float speedRot = 10;
    public int _currentIndex = 0;
    public float _avoidanceWeight = 1;
    public float _steeringWeight = 1;
    public float _predictionTime = 2;
    public int maxObs = 1;
    int _nextPoint = 0;
    public int damage = 10;
    public float _walkcounter;
    internal Vector3 _dir;
    public ISteering _currentSteering;
    public ISteering _avoidance;

    public bool canSeePlayer;
    public bool isWounded;
    public bool Leader1died;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //anim = GetComponent<EnemyView>();
        agentController = FindObjectOfType<SecondAgentController>();

    }
    private void Start()
    {
        _avoidance = new ObstacleAvoidance(transform, obsRadius, maxObs, obsAngle, obstacleMask);
    }
    public void LeaderIsDead()
    {
        Leader1died = false;
    }
    public void CanSeePlayer()
    {
        canSeePlayer = true;
    }
    public void CantSeePlayer()
    {
        canSeePlayer = false;
    }
    public void Dead()
    {
        Destroy(this.gameObject);
    }
    //ON-COLLISION-ENTER
    private void OnCollisionEnter(Collision collision)
    {
        var entity = collision.gameObject.GetComponent<IEntity>();
        if (entity != null)
        {
            OnCollision();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("Enemy dead");
            Destroy(this.gameObject);
        }
    }
    //GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, obsRadius);
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, obsAngle / 2, 0) * transform.forward * obsRadius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -obsAngle / 2, 0) * transform.forward * obsRadius);
    }
}
