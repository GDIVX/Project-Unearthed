using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanReachDestination", menuName = "UtilityAI/Considerations/Can Reach Destination")]
public class CanReachDestination : Consideration
{
    public override float ScoreConsideration(UtilityAgent Agent)
    {
        int rand = Random.Range(0, 2);
        return rand;
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
