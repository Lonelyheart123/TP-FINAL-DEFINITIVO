using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeLeaderController : MonoBehaviour
{
    public PathfindingEnemyModel _enemy;
    public FSM<states> _fsm;
    public Transform target;
    OppositeLeaderModel opLeaderModel;
    public SecondAgentController agentController;

    public float attackRange;
    private float dist;
    private ITreeNode _root;
    Vector3 dir;

    private void Awake()
    {
        _enemy = GetComponent<PathfindingEnemyModel>();
        target = GameObject.FindGameObjectWithTag("Leader1").transform;
        agentController = GetComponent<SecondAgentController>();
        //opLeaderModel = FindObjectOfType<OppositeLeaderModel>();
        _fsm = new FSM<states>();
        opLeaderModel = GetComponent<OppositeLeaderModel>();
    }

    public enum states
    {
        Patrol,
        Escape,
        Attack,
        Chase,
        Died,
        Evade
    }
    private void Start()
    {
        //opLeaderModel = FindObjectOfType<OppositeLeaderModel>();
        InitializedTree();
        InitializedFSM();
    }
    void InitializedFSM()
    {
        IState<states> patrol = new OppositeLeaderPatrol<states>(opLeaderModel, target, _root, _enemy._currentSteering);
        IState<states> chase = new OppositeLeaderChase<states>(opLeaderModel, this, _enemy, _root, _enemy.transform, _enemy._avoidance);
        IState<states> attack = new OppositeLeaderAttack<states>(opLeaderModel, this, _enemy, dist, dir, _root);
        IState<states> escape = new OppositeLeaderEscape<states>(_enemy, this, _enemy._currentSteering, opLeaderModel);
        IState<states> died = new OppositeLeaderDie<states>(opLeaderModel);

        patrol.AddTransition(states.Chase, chase);
        patrol.AddTransition(states.Attack, attack);
        patrol.AddTransition(states.Died, died);
        // patrol.AddTransition(states.Escape, escape);

        chase.AddTransition(states.Attack, attack);
        chase.AddTransition(states.Patrol, patrol);
        chase.AddTransition(states.Died, died);

        attack.AddTransition(states.Chase, chase);
        // attack.AddTransition(states.Escape, escape);
        attack.AddTransition(states.Patrol, patrol);
        attack.AddTransition(states.Died, died);

        _fsm.SetInit(patrol);
    }
    void InitializedTree()
    {
        //Actions

        ITreeNode died = new TreeAction(() => _fsm.Transitions(states.Died));
        ITreeNode escape = new TreeAction(() => _fsm.Transitions(states.Escape));
        ITreeNode attack = new TreeAction(() => _fsm.Transitions(states.Attack));
        ITreeNode chase = new TreeAction(() => _fsm.Transitions(states.Chase));
        ITreeNode patrol = new TreeAction(() => _fsm.Transitions(states.Patrol));
        //Questions

        //ITreeNode qIsLeaderDead = new TreeQuestion(IsDead, died, attack);
        //ITreeNode qIdleCounter = new TreeQuestion(IsCounter, patrol, chase);

        ITreeNode qIsEnemyClose = new TreeQuestion(ShootRange, attack, chase);
        ITreeNode qLineOfSight = new TreeQuestion(LineOfSight, qIsEnemyClose, patrol);
        ITreeNode qIsWounded = new TreeQuestion(IsWounded, escape, qLineOfSight);
        ITreeNode qIsAlive = new TreeQuestion(IsAlive, qIsWounded, died);//PUNTO DE PARTIDA

        _root = qIsAlive;
    }
    public bool IsAlive() => _enemy.isAlive;
    public bool LineOfSight() { print("CHAU " + _enemy.canSeePlayer); return _enemy.canSeePlayer; }
    public bool IsDead() { return _enemy.Leader1died; }
    public bool IsWounded() { return _enemy.isWounded; }
    public bool ShootRange() { bool isShootRange = (Vector3.Distance(transform.position, target.position) <= attackRange) ? true : false; print("HOLA " + isShootRange); return isShootRange; }
    public bool IsCounter() { return _enemy.readyToMove; }
    void Update()
    {
        _fsm.OnUpdate();
    }
    //DestroyEnemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Enemy dead");
            Destroy(this.gameObject);
        }
    }
}
