using Skul.Movement;
using UnityEngine;
using Skul.FSM;


public class EnemyAI:MonoBehaviour
{
    enum AIStep
    {
        Idle,
        Think,
        TakeARest,
        MoveLeft,
        MoveRight,
        StartChase,
        Chase,
        StartAttack,
        Attack,
    }

    [SerializeField] private bool isGiant;

    [SerializeField] private AIStep _step;
    [SerializeField] private bool _autoFollow;
    [SerializeField] private LayerMask _detectMask;
    [SerializeField] private float _detectRange = 1.0f;
    [SerializeField] private bool _attackEnable;
    [SerializeField] private float _attackRange = 1.0f;
    [SerializeField] private float _thinkTimeMin = 0.1f;
    [SerializeField] private float _thinkTimeMax = 2.0f;
    [SerializeField] private float _thinkTimer;
    [SerializeField] private Vector3 _detRangeOffset;
    [SerializeField] private Vector3 _attRangeOffset;

    [Header("TESTSize")]
    [SerializeField] private Vector2 _detectCube;
    [SerializeField] private Vector2 _attackCube;

    [Header("TESTPos")]
    [SerializeField] private float posPran;
    [SerializeField] private float posDran;
    [SerializeField] private float taPposPran;
    [SerializeField] private float taPposDran;
    [SerializeField] private Vector3 _targetPos;


    [Header("CoolTime")]
    [SerializeField] private float _hitCoolTime;
    [SerializeField] private float _hitCoolTimeMax = 2.0f;

    public GameObject target;
    private StateMachine _stateMachine;
    [SerializeField]private EnemyMovement _movement;
    private BoxCollider2D _collider;
    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _movement = GetComponent<EnemyMovement>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        //Collider2D target = Physics2D.OverlapCircle(transform.position + new Vector3(_rangeOffset.x*_movement.direction,_rangeOffset.y), _detectRange, _detectMask);
        Collider2D target = Physics2D.OverlapBox(transform.position + new Vector3(_detRangeOffset.x*_movement.direction,_detRangeOffset.y), _detectCube,0, _detectMask);
        this.target = target?target.gameObject:null;
        if (this.target)
            _targetPos = this.target.transform.position;
        else
            _targetPos = Vector3.zero ;

        posPran = (transform.position.x + _attackRange);
        posDran = (transform.position.x - _attackRange);
        //taPposPran = target.transform.position.x + (transform.position.x + _attackRange);
        //taPposDran = target.transform.position.x + (transform.position.x - _attackRange);

        _hitCoolTime += Time.deltaTime;
        if(_hitCoolTime>=_hitCoolTimeMax)
            _hitCoolTime= _hitCoolTimeMax+1;

        if (_autoFollow &&
            _step < AIStep.StartChase &&
            this.target)
        {
            _step = AIStep.StartChase;
        }

        if (_stateMachine.isDie == true)
            _step = AIStep.Idle;
        


        switch (_step)
        {
            case AIStep.Idle:
                break;
            case AIStep.Think:
                {
                    _step = (AIStep)Random.Range((int)AIStep.TakeARest, (int)AIStep.MoveRight + 1);
                    _thinkTimer = Random.Range(_thinkTimeMin, _thinkTimeMax);

                    _stateMachine.ChangeState(_step == AIStep.TakeARest ? StateType.Idle : StateType.Move);

                }
                break;
            case AIStep.TakeARest:
                {
                    _movement.horizontal = 0.0f;
                    if (_thinkTimer > 0)
                        _thinkTimer -= Time.deltaTime;
                    else
                        _step = AIStep.Think;
                }
                break;
            case AIStep.MoveLeft:
                {

                    _movement.direction = Movement.DIRECTION_LEFT;
                    _movement.horizontal = -1.0f;
                    if (_thinkTimer > 0)
                        _thinkTimer -= Time.deltaTime;
                    else
                        _step = AIStep.Think;
                }
                break;
            case AIStep.MoveRight:
                {

                    _movement.direction = Movement.DIRECTION_RIGHT;
                    _movement.horizontal = 1.0f;
                    if (_thinkTimer > 0)
                        _thinkTimer -= Time.deltaTime;
                    else
                        _step = AIStep.Think;
                }
                break;
            case AIStep.StartChase:
                {
                    _stateMachine.ChangeState(StateType.Move);
                    _step= AIStep.Chase;
                }
                break;
            case AIStep.Chase:
                {
                    if(target==null)
                    {
                        _step = AIStep.Think;
                        return;
                    }
                    //if (transform.position.x < target.transform.position.x - _collider.size.x))
                    //if (transform.position.x+_attackRange < target.transform.position.x - (transform.position.x+_attackRange))
                    if (posPran < _targetPos.x)
                    {
                        Debug.Log("오른쪽 사거리밖");
                        _movement.direction = Movement.DIRECTION_RIGHT;
                        _movement.horizontal = 1.0f; 
                    }
                    else if (posDran > _targetPos.x)
                    {
                        Debug.Log("왼쪽 사거리밖");
                        _movement.direction = Movement.DIRECTION_LEFT;
                        _movement.horizontal = -1.0f;
                    }
                    else if(_targetPos.x>posDran&&_targetPos.x<transform.position.x)
                    {
                        Debug.Log("왼쪽 사거리안");
                        _movement.direction = Movement.DIRECTION_LEFT;
                        _movement.horizontal = 0;
                    }
                    else if( _targetPos.x < posPran&&_targetPos.x>transform.position.x)
                    {
                        Debug.Log("오른쪽 사거리안");
                        _movement.direction = Movement.DIRECTION_RIGHT;
                        _movement.horizontal = 0;
                    }
                    //else if (transform.position.x > target.transform.position.x + _collider.size.x)
                    //else if (transform.position.x - _attackRange > target.transform.position.x + (transform.position.x - _attackRange))


                    if (_attackEnable &&
                        Vector2.Distance(transform.position, target.transform.position) < _attackRange &&
                        target.transform.position.y < transform.position.y+_attackCube.y &&
                        _hitCoolTime>=_hitCoolTimeMax)
                    {
                        _step = AIStep.StartAttack;
                    }
                }
                break;
            case AIStep.StartAttack:
                {
                    _movement.isMovable = false;
                    _movement.horizontal = 0.0f;
                    _stateMachine.ChangeState(StateType.Attack);
                    _step = AIStep.Attack;
                }
                break;
            case AIStep.Attack:
                {
                    _movement.isMovable = false;
                    _movement.horizontal = 0.0f;
                    if (_stateMachine.currentType != StateType.Attack)
                    {
                        _movement.isMovable = true;
                        _hitCoolTime = 0;
                        _step = AIStep.Think; 
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (_autoFollow)
        {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(transform.position+ new Vector3(_rangeOffset.x * _movement.direction, _rangeOffset.y), _detectRange);
            Gizmos.DrawWireCube(transform.position+ new Vector3(_detRangeOffset.x * _movement.direction, _detRangeOffset.y), _detectCube);
        }

        if (_attackEnable)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position+ new Vector3(_rangeOffset.x * _movement.direction, _rangeOffset.y), _attackRange);
            Gizmos.DrawWireCube(transform.position+ new Vector3(_attRangeOffset.x * _movement.direction, _attRangeOffset.y), _attackCube);
        }

        Gizmos.color= Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + _attackCube.y));
    }
}