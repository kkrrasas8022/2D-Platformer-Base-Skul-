
using Skul.Character.PC;
using Skul.Movement;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Skul.Character
{
    public class EdgeDetecter:MonoBehaviour
    {
        [SerializeField] private Movement.Movement _movement;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector2 _size;

        [SerializeField] private Collider2D _col;

        [SerializeField] public bool _edgeEnd;
        public bool isDetected => detected;
        public Collider2D detected
        {
            get
            {
                return _detected;
            }
            private set
            {
                if (detected == value)
                    return;

                _isDetected = value;
                _detected = value;
            }
        }
        [SerializeField] private bool _isDetected;
        [SerializeField] private Collider2D _detected;
        [SerializeField] private LayerMask groundDe;

        private void Awake()
        {
            _movement = GetComponent<Movement.Movement>();
        }

        private void FixedUpdate()
        {
            detected = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(_offset.x * _movement.direction,  _offset.y),
                                                         _size,
                                                         0.0f,
                                                         groundDe);
            if(detected)
                _edgeEnd = false;
            else
                _edgeEnd= true;
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position+ new Vector3(_offset.x * _movement.direction, _offset.y), _size);
                //(_movement.direction==1?_offset:_offset*(-1.0f)), _size);
        }
    }
}