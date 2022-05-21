// Roman Baranov 21.05.2022

using UnityEngine;

public class AI_DeathState : AI_State
{
    #region STATES
    public AI_StateId GetId()
    {
        return AI_StateId.Death;
    }

    public void Enter(AI_Agent agent)
    {
       
    }

    public void Update(AI_Agent agent)
    {
    }

    public void Exit(AI_Agent agent)
    {
    }
    #endregion
}
