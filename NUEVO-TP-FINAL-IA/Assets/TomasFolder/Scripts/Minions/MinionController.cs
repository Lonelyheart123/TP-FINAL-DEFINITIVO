using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{

    //public OpposingMinionController[] _opposingMinionCtrl;
    public OppositeLeaderModel _oppositeleaderCtrl;
    public PathfindingEnemyController _leaderController;
    MinionModel _minionModel;
    FSM<MinionStateEnum> _fsm;
    ITreeNode _root;

    private void Awake()
    {
        //_minionModel.currentSterring = new Pursuit(this.transform, _oppositeleaderCtrl, 2f);
        //_opposingMinionCtrl = FindObjectsOfType<OpposingMinionController>();
        _oppositeleaderCtrl = FindObjectOfType<OppositeLeaderModel>();
        _leaderController = FindObjectOfType<PathfindingEnemyController>();
        _minionModel = GetComponent<MinionModel>();
    }
    private void Start()
    {
        IntializedFSM();
        InitializedTree();
    }

    public void IntializedFSM()
    {
        var list = new List<MinionStateBase<MinionStateEnum>>();// LISTA DE ESTADOS
        _fsm = new FSM<MinionStateEnum>();// NUEVO FSM MINIONS
        
        var died = new MinionDiedState<MinionStateEnum>();
        var escape = new MinionEscapeState<MinionStateEnum>();
        var follow = new MinionFollowState<MinionStateEnum>(_oppositeleaderCtrl);
        var chase = new MinionChaseState<MinionStateEnum>(_minionModel.transform, _oppositeleaderCtrl, _minionModel.currentSterring);
        var attack = new MinionAttackState<MinionStateEnum>(_minionModel.transform, _oppositeleaderCtrl);

        list.Add(died);
        list.Add(escape);
        list.Add(follow);
        list.Add(chase);
        list.Add(attack);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(_minionModel, _fsm);
        }

        escape.AddTransition(MinionStateEnum.FollowLeader, follow);
        escape.AddTransition(MinionStateEnum.Died, died);
        escape.AddTransition(MinionStateEnum.Chase, chase);
        escape.AddTransition(MinionStateEnum.Attack, attack);
        follow.AddTransition(MinionStateEnum.Chase, chase);
        follow.AddTransition(MinionStateEnum.Escape, escape);
        follow.AddTransition(MinionStateEnum.Attack, attack);
        chase.AddTransition(MinionStateEnum.Died, died); // Revisar si es necesario
        chase.AddTransition(MinionStateEnum.Escape, escape);
        chase.AddTransition(MinionStateEnum.FollowLeader, follow);
        chase.AddTransition(MinionStateEnum.Attack, attack);
        //
        attack.AddTransition(MinionStateEnum.FollowLeader, follow);
        attack.AddTransition(MinionStateEnum.Chase, chase);
        attack.AddTransition(MinionStateEnum.Escape, escape);
        attack.AddTransition(MinionStateEnum.Died, died);

        _fsm.SetInit(follow);
    }
    public void InitializedTree()
    {
        //ACTIONS
        var died = new TreeAction(ActionDied);
        var escape = new TreeAction(ActionEscape);
        var follow = new TreeAction(ActionFollow);
        var chase = new TreeAction(ActionChase);
        var attack = new TreeAction(ActionAttack);

        //QUESTIONS
        var enemyInAtkRange = new TreeQuestion(EnemyInAtkRange, attack, chase);
        var enemyDetected = new TreeQuestion(EnemyDetected, enemyInAtkRange, follow);
        var boxInAtkRange = new TreeQuestion(BoxInAtkRange, chase, escape);
        var boxDetected = new TreeQuestion(BoxDetected, boxInAtkRange, escape);
        var potionDetected = new TreeQuestion(PotionDetected, chase, boxDetected);
        var itsHurt = new TreeQuestion(ItsHurt, potionDetected, enemyDetected);
        var itsAlive = new TreeQuestion(ItsAlive, itsHurt , died);

        _root = itsAlive;
    }

    #region QUESTIONS-MINION
    bool ItsAlive() => _minionModel.itsAlive;
    bool ItsHurt() => _minionModel.itsHurt;
    bool EnemyDetected() => _minionModel.enemyDetected;
    bool EnemyInAtkRange() => _minionModel.enemyInAtkRange;
    bool PotionDetected() => _minionModel.potionDetected;
    bool BoxDetected() => _minionModel.boxDetected;
    bool BoxInAtkRange() => _minionModel.boxInAtkRange;

    #endregion

    #region ACTIONS-MINION
    void ActionDied() => _fsm.Transitions(MinionStateEnum.Died);
    void ActionEscape() => _fsm.Transitions(MinionStateEnum.Escape);
    void ActionFollow() => _fsm.Transitions(MinionStateEnum.FollowLeader);
    void ActionChase() => _fsm.Transitions(MinionStateEnum.Chase);
    void ActionAttack() => _fsm.Transitions(MinionStateEnum.Attack);
    #endregion


    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
        if (_minionModel.currentHP > 0) { _minionModel.itsAlive = true; }
    }
}
