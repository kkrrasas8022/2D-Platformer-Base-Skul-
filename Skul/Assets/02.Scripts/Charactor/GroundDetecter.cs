using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character
{
    //객체의 바닥과의 접촉여부를 판정하는 클래스
    public class GroundDetecter:MonoBehaviour
    {
        public bool isDetected => detected;
        private Collider2D detected
        {
           
        }



        [SerializeField]private bool _isDetected;
        private Collider2D _detected;
        private Collider2D _latest;
        private List<Collider2D> _ignorings=new List<Collider2D>();
        [SerializeField] private Vector2 _castOffset;
        [SerializeField] private Vector2 _castSize;
        [SerializeField] private LayerMask _groundMask;
    }
}