using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using UnityEngine.AI;
using Assets.Scripts.CharacterAbilities;
using System.Linq;
using Assets.Scripts.GameManagers;
/// <summary>
/// Patrol
/// </summary>
[CreateAssetMenu(fileName = "Patrol", menuName = "AI/Actions/Patrol")]
public class Patrol : UtilityAction
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Movement _move;
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] float _scanRadius;
    int _wayPointIndex = 0;
    int wayPointIndex { get { return _wayPointIndex; } set { _wayPointIndex = value; } }  
    Vector3 _targetWayPoint;
    public override void Execute(GameObject agent)
    {
        _agentGameObject = agent;
        if(_move==null)
        {
            _move = agent.gameObject.GetComponent<Movement>();
            _agent = agent.gameObject.GetComponent<NavMeshAgent>();
            UpdateDestination();//fix
        }
        GoToDestination();
        Debug.Log("patroling");
        Debug.Log($"_wayPointIndex: {_wayPointIndex}");
    }

    void GoToDestination()
    {
        if (Vector3.Distance(_agentGameObject.transform.position, _targetWayPoint) < 1)
        {
            IterateWayPointIndex();
            UpdateDestination();
        }
        ScanForTarget();
    }
    void UpdateDestination()
    {
        _targetWayPoint = _wayPoints[_wayPointIndex].position;
        _agent.SetDestination(_targetWayPoint);
    }

    void IterateWayPointIndex()
    {
        _wayPointIndex++;
        if (_wayPointIndex == _wayPoints.Length)
        {
            _wayPointIndex = 0;
        }
    }

    bool ScanForTarget()//do this scan every 0.5 second or other number
    {
        Collider potentialTargets;
        LayerMask layerMask = GameManager.Instance.AICanSee;
        potentialTargets = Physics.OverlapSphere(_agentGameObject.transform.position, _scanRadius, layerMask).FirstOrDefault();//PLAYER ONLY
        if(potentialTargets != null)
        {
            _target = potentialTargets.gameObject;
            return true;
        }
        return false;

    }
    protected override float CalculateUtilityScore()
    {
        if (_target!=null)
            _score = 0;
        else
            _score = 1;
        _score = Mathf.Clamp(_score, 0, 1);
        return _score;
        //throw new System.NotImplementedException();
    }        
}
