using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character
{
    //��ü�� �ٴڰ��� ���˿��θ� �����ϴ� Ŭ����
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