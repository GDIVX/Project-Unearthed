using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public LayerMask AICanSee;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}
