using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using Assets.Scripts.CharacterAbilities;
using UnityEngine.AI;
using Assets.Scripts.GameManagers;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
/// <summary>
/// Pursue
/// </summary>
[CreateAssetMenu(fileName = "Pursue", menuName = "AI/Actions/Pursue")]
public class Pursue : UtilityAction
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Movement _move;
    [SerializeField] float _scanRadius;
    

    public override void Execute(GameObject agent)
    {
        _agentGameObject = agent;
        if (_move == null)
        {
            _move = agent.gameObject.GetComponent<Movement>();
            _agent = agent.gameObject.GetComponent<NavMeshAgent>();
        }
        ScanForTarget();
        _agent.SetDestination(_target.transform.position);
        Debug.Log("pursue player");
    }
    bool ScanForTarget()//do this scan every 0.5 second or other number
    {
        Collider potentialTargets;
        LayerMask layerMask = GameManager.Instance.AICanSee;
        potentialTargets = Physics.OverlapSphere(_agentGameObject.transform.position, _scanRadius, layerMask).FirstOrDefault();//PLAYER ONLY
        if (potentialTargets != null)
        {
            _target = potentialTargets.gameObject;
            return true;
        }
        return false;

    }
    protected override float CalculateUtilityScore()
    {
        if (_target!=null)
            _score = 1;
        else
            _score = 0;
        _score = Mathf.Clamp(_score, 0, 1);
        return _score;
    }        
}
