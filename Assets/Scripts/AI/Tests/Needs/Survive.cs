using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Survive", menuName = "UtilityAI/Needs/Survive")]
public class Survive : Need
{
    protected override float CalculateUtilityScore()
    {
        //Type type = GetType();
        //MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => !m.IsSpecialName).ToArray();
        //int count = 0;
        //float total = 0f;
        //foreach (MethodInfo method in methods)
        //{
        //    if (method.ReturnType == typeof(float) && method.Name != nameof(CalculateUtilityScore))
        //    {
        //        total += (float)method.Invoke(this, null);
        //        count++;
        //    }
        //}

        int count = 2;
        float total = 0f;
        total += FirstCondition() + SecondCondition();

        if (count > 0)
        {
            return total / count;
        }
        else
        {
            return 0f;
        }
    }

    public float FirstCondition()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }
    public float SecondCondition()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }
}
