// Roman Baranov 21.05.2022

public class AI_MoveToPointState : AI_State
{
    #region STATES
    public AI_StateId GetId()
    {
        return AI_StateId.MoveToPoint;
    }

    public void Enter(AI_Agent agent)
    {
        agent.NavMeshAgent.speed = agent.Speed;
    }

    public void Update(AI_Agent agent)
    {
        // Player reached point
        if (agent.NavMeshAgent.remainingDistance <= 0.01f)
        {
            // Switch to find point state
            agent.StateMachine.ChangeState(AI_StateId.FindPoint);
            return;
        }
    }

    public void Exit(AI_Agent agent)
    {
    }
    #endregion
}
