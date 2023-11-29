using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Skul.Character.PC
{
    public class Chains : MonoBehaviour
    {
        [SerializeField] public PlayerProjectile[] cains;
        private void Awake()
        {
            GetComponent<Animator>().Play("Move");
            cains=GetComponentsInChildren<PlayerProjectile>();
        }

        private void Update()
        {
            if (cains[0] == null)
                Destroy(gameObject);
        }
    }
}