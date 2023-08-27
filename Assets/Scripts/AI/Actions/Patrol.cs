using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using UnityEngine.AI;
using Assets.Scripts.CharacterAbilities;
using System.Linq;
using Assets.Scripts.GameManagers;
using Sirenix.OdinInspector;
/// <summary>
/// Patrol
/// </summary>
public class Patrol : UtilityAction
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Movement _move;
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] float _scanRadius;
    int _wayPointIndex = 0;
    int wayPointIndex { get { return _wayPointIndex; } set { _wayPointIndex = value; } }
    protected override ValueDropdownList<string> myScoreValuesList { get; set; } = new ValueDropdownList<string>()
    {
        { "HasTarget", "HasTarget"}
    };
    Vector3 _targetWayPoint;

    public override void Execute(GameObject gameObject)
    {
        if(_move==null)
        {
            SetScoreValues();
            _move = gameObject.GetComponent<Movement>();
            _agent = gameObject.GetComponent<NavMeshAgent>();
            UpdateDestination();
        }
        GoToDestination();
        //Debug.Log("patroling");
        //Debug.Log($"_wayPointIndex: {_wayPointIndex}");
    }

    void GoToDestination()
    {
        if (Vector3.Distance(_agent.transform.position, _targetWayPoint) < 1)
        {
            IterateWayPointIndex();
            UpdateDestination();
            //Debug.Log("change target");
        }
    
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
        if(_agent==null)
            return false;
        Collider potentialTargets;
        LayerMask layerMask = GameManager.Instance.AICanSee;
        potentialTargets = Physics.OverlapSphere(_agent.transform.position, _scanRadius, layerMask).FirstOrDefault();//PLAYER ONLY
        if(potentialTargets != null)
        {
            _target = potentialTargets.gameObject;
            return true;//found target
        }
        return false; //didnt found target

    }
    protected override float CalculateUtilityScore()
    {
        if (myScoreValues == null && myScoreValues.Count == 0)
            return 1;
        float actionScore = ScanForTarget() ? 0f : 1f;
        myScoreValues["HasTarget"] = actionScore;
        //add here score values
        _actionScore = myScoreValues.Values.Average();
        if (_actionScore == 0)
        {
            _move = null;
            _agent.GetComponent<UtilityAgent>().Needs.FirstOrDefault(c => c as PlayerAwareness).AnimationCurveSet(0.1f);
            //_actionScore = _agent.GetComponent<UtilityAgent>().Needs.FirstOrDefault(c => c is PlayerAwareness).AnimationCurveSet(11);
        }
        TargetNeed.AnimationCurveSet(_actionScore);
        return _scoreCurve.Evaluate(_actionScore);
    }

}
