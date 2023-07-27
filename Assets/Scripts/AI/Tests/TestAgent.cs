using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAgent : UtilityAgent
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ExecuteAction", 2f, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
