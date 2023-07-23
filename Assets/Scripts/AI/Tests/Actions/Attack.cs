using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "UtilityAI/Actions/Attack")]
public class Attack : UtilityAction
{
    public override void Execute(UtilityAgent agent)
    {
        Debug.Log("Chose to attack");
    }

    protected override float CalculateUtilityScore()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
