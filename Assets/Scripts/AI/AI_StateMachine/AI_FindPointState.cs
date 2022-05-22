// Roman Baranov 21.05.2022

using UnityEngine;
using UnityEngine.AI;

public class AI_FindPointState : AI_State
{
    #region VARIABLES
    private float _getPointRadius = 10f;
    #endregion

    #region STATES
    public AI_StateId GetId()
    {
        return AI_StateId.FindPoint;
    }

    public void Enter(AI_Agent agent)
    {
        Vector3 destination = GetRandomPoint(agent);
        agent.NavMeshAgent.SetDestination(destination);
        agent.StateMachine.ChangeState(AI_StateId.MoveToPoint);
        return;
    }

    public void Update(AI_Agent agent)
    {
    }
    public void Exit(AI_Agent agent)
    {
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Gets random naw mesh point around the agent
    /// </summary>
    /// <param name="agent">Agent to find path for</param>
    /// <returns></returns>
    private Vector3 GetRandomPoint(AI_Agent agent)
    {
        Vector3 point = Vector3.zero;
        bool isPointReachable = false;

        while (!isPointReachable)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * _getPointRadius, out navMeshHit, _getPointRadius, NavMesh.AllAreas);

            point = navMeshHit.position;
            NavMeshPath path = new NavMeshPath();
            //agent.NavMeshAgent.CalculatePath(point, path);
            //if (path.status == NavMeshPathStatus.PathComplete)
            if (agent.NavMeshAgent.CalculatePath(point, path))
            {
                isPointReachable = true;
            }
        }

        return point;
    }
    #endregion
}
