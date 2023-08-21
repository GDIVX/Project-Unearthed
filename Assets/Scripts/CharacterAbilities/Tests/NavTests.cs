using Assets.Scripts.CharacterAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CharacterAbilities.Assets.Scripts.CharacterAbilities;

public class NavTests : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Movement _move;
    [SerializeField] Transform _targetTransform;
    [SerializeField] Transform[] _wayPoints;

    int _wayPointIndex;
    Vector3 _targetWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        _agent.autoBraking = false;
        if (_targetTransform == null)
        {
            Debug.LogError("Target transform not assigned in NavMeshMovementController.");
        }
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _targetWayPoint) < 1)
        {
            IterateWayPointIndex();
            UpdateDestination();
        }

        //Vector3 inputVector = (_targetTransform.position - transform.position).normalized;
        //_move.Move(inputVector);

        _agent.velocity = _move.CurrentSpeed * (transform.forward.normalized);
        Debug.Log(_agent.destination);
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
}

