using Cinemachine;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Perspective : MonoBehaviour
{
    [SerializeField] Vector3 _currentPerspective;
    [SerializeField] List<Transform> _objectsToKeepInPerspective;

    public UnityEvent<Vector3> OnPerspectiveChage;

    //singelton
    public static Perspective Instance { get; private set; }

    public Vector3 CurrentPerspective
    {
        get => _currentPerspective;

        set
        {
            _currentPerspective = value;
            RotateObjects();
            OnPerspectiveChage?.Invoke(value);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        RotateObjects();
    }

    [Button]
    private void RotateObjects()
    {
        foreach (Transform _transform in _objectsToKeepInPerspective)
        {
            _transform.rotation = Quaternion.Euler(CurrentPerspective);
        }
    }
}
