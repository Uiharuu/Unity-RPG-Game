using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Fall : Player_State_Aired
{
    public Player_State_Fall(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.wallCheck)
            stateMachine.ChangeState(player.wallSlideState);
        else if (player.groundedCheck)
            stateMachine.ChangeState(player.idleState);


    }
}
