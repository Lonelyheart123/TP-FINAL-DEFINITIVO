using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentController : MonoBehaviour
{   
    public Node goalNode;
    public Node startNode;
    public Node firstEscapeNode;
    public Node escapeNode;
    public float radius;
    Collider[] _colliders;
    public LayerMask mask;
    public AStar<Node> _ast;
    public List<Transform> AllNodes;
    public PathfindingEnemyModel leaderModel;

    int randomNode;
    private void Awake()
    {
        _ast = new AStar<Node>();
        _colliders = new Collider[10];
        //AllGoals = new List<Node>();
    }
    private void Start()
    {

    }
    public void AStarRun()
    {
        var start = startNode;
        if (start == null) return;

        var path = _ast.Run(start, Satisfies, GetConections, GetCost, Heuristic, 500);

        leaderModel.SetWayPoints(path);
    }
    public void EscapeRun()
    {
        randomNode = Random.Range(0, AllNodes.Count);

        var start = GetStartNode(AllNodes[randomNode].position);
        if (start == null) return;

        var path = _ast.Run(start, EscapeSatisfies, GetConections, GetCost, HeuristicEscape, 500);

        leaderModel.SetWayPoints(path);
    }
    float Heuristic(Node curr)
    {
        float multiplierDistance = 2;
        float cost = 0;
        cost += Vector3.Distance(curr.transform.position, goalNode.transform.position) * multiplierDistance;
        return cost;
    }
    float HeuristicEscape(Node curr)
    {
        float multiplierDistance = 2;
        float cost = 0;
        cost += Vector3.Distance(curr.transform.position, escapeNode.transform.position) * multiplierDistance;
        return cost;
    }
    float GetCost(Node parent, Node son)
    {
        float multiplierDistance = 1;
        //float multiplierEnemies = 20;
        float multiplierTrap = 20;

        float cost = 0;
        cost += Vector3.Distance(parent.transform.position, son.transform.position) * multiplierDistance;
        //if (son.hasTrap)
        //    cost += multiplierTrap;
        //cost += 100 * multiplierEnemies;
        return cost;
    }
    List<Node> GetConections(Node curr)
    {
        return curr.neighbours;
    }
    bool Satisfies(Node curr)
    {
        return curr == goalNode;
    }
    bool EscapeSatisfies(Node curr)
    {
        return curr == escapeNode;
    }
    public Node GetStartNode(Vector3 enemy)
    {
        int count = Physics.OverlapSphereNonAlloc(enemy, radius, _colliders, mask);
        float bestDistance = 0;
        Collider bestCollider = null;
        for (int i = 0; i < count; i++)
        {
            Collider currColl = _colliders[i];
            float currDistance = Vector3.Distance(enemy, currColl.transform.position);
            if (bestCollider == null || bestDistance > currDistance)
            {
                bestDistance = currDistance;
                bestCollider = currColl;
            }
        }
        if (bestCollider != null)
        {
            return bestCollider.GetComponent<Node>();
        }
        else
        {
            return null;
        }
    }
}