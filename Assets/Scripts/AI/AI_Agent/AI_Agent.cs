// Roman Baranov 21.05.2022

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Agent : MonoBehaviour
{
    #region VARIABLES
    [Min(1)]
    [Header("Enemy movement speed")]
    [SerializeField] private float _speed = 1f;
    /// <summary>
    /// Agent movement speed
    /// </summary>
    public float Speed { get { return _speed; } }

    [Min(1)]
    [Header("Enemy damage amount to player")]
    [SerializeField] private int _damage = 1;
    /// <summary>
    /// Agent damage
    /// </summary>
    public int Damage { get { return _damage; } }

    /// <summary>
    /// Initial agent state
    /// </summary>
    [SerializeField] private AI_StateId _initialState;

    private AI_StateMachine _stateMachine = null;
    /// <summary>
    /// Agent's state machine referrence
    /// </summary>
    public AI_StateMachine StateMachine { get { return _stateMachine; } }

    private NavMeshAgent _navMeshAgent = null;

    /// <summary>
    /// NavMeshAgent component of the game object
    /// </summary>
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } }
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        // State machine
        _stateMachine = new AI_StateMachine(this);
        _stateMachine.RegisterState(new AI_FindPointState());
        _stateMachine.RegisterState(new AI_MoveToPointState());
        _stateMachine.RegisterState(new AI_DeathState());

        _stateMachine.ChangeState(_initialState);
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
    }
    #endregion
}
