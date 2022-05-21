// Roman Baranov 21.05.2022

using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _speed = 1f;
    [SerializeField] private LayerMask _moveableLayers;

    private NavMeshAgent _agent = null;
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (_agent != null)
        {
            _agent.speed = _speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Check if there ara any colliders
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, _moveableLayers))
                {
                    // Move player to position
                    _agent.SetDestination(hitInfo.point);

                    //Send Callback OnPlayerMove
                    GameplayEvents.OnPlayerMove.Invoke();
                    return;
                }
            }
        }
    }
    #endregion
}
