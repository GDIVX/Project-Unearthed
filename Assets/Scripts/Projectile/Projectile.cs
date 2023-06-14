using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        public GameObject GameObject => gameObject;

        public void OnGet()
        {

        }

        public void OnReturn()
        {
        }
    }
}