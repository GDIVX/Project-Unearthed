using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
using Assets.Scripts.CharacterAbilities;
using UnityEngine.AI;
/// <summary>
/// Pursue
/// </summary>
[CreateAssetMenu(fileName = "Pursue", menuName = "AI/Actions/Pursue")]
public class Pursue : UtilityAction
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Movement _move;
    public override void Execute(GameObject agent)
    {
        
    }

    protected override float CalculateUtilityScore()
    {
       throw new System.NotImplementedException();
    }        
}
