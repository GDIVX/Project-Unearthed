using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using Assets.Scripts.CharacterAbilities;
using UnityEngine.AI;
using Assets.Scripts.GameManagers;
using System.Linq;
using Sirenix.OdinInspector;
/// <summary>
/// Pursue
/// </summary>
public class Pursue : UtilityAction
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Movement _move;
    [SerializeField] float _scanRadius;

    protected override ValueDropdownList<string> myScoreValuesList { get; set; } = new ValueDropdownList<string>()
    {
        { "Distance", "Distance"}
    };

    public override void Execute(GameObject gameObject)
    {
        if (_move == null)
        {
            SetScoreValues();
            _move = gameObject.GetComponent<Movement>();
            _agent = gameObject.GetComponent<NavMeshAgent>();
        }
        ScanForTarget();
        Debug.Log("pursue player");
        UpdateDestination();
    }
    bool ScanForTarget()//do this scan every 0.5 second or other number
    {
        Collider potentialTargets;
        LayerMask layerMask = GameManager.Instance.AICanSee;
        potentialTargets = Physics.OverlapSphere(_agent.transform.position, _scanRadius, layerMask).FirstOrDefault();//PLAYER ONLY
        if (potentialTargets != null)
        {
            _target = potentialTargets.gameObject;
            return true;
        }
        _target = null;
        return false;

    }
    void UpdateDestination()
    {
        if(_target!=null)
        _agent.SetDestination(_target.transform.position);
    }
    protected override float CalculateUtilityScore()
    {
        if (myScoreValues == null && myScoreValues.Count == 0)
            return 1;
        float distanceFromTarget = 0;
        if (_target != null && _agent!=null)
            distanceFromTarget = Vector3.Distance(_target.transform.position, _agent.transform.position);
        myScoreValues["Distance"] = distanceFromTarget;
        _actionScore = myScoreValues.Values.Average();
        TargetNeed.AnimationCurveSet(_actionScore);
        if (_agent != null && (_actionScore<0.2f))
            _agent.GetComponent<UtilityAgent>().Needs.FirstOrDefault(c => c is Idle).AnimationCurveSet(1);

        return _actionScore;
        //_score = Mathf.Clamp(_score, 0, 1);
        //return _score;
    }        
}
