using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDodge : StateBase
{
    int dodgeCount;

    public StateDodge(Player player) : base(player)
    {
        
    }

    public override void Tick()
    {
        if (dodgeCount != 0)
        {
            dodgeCount -= 1;
            player.rb.velocity = new Vector2(player.dodgeSpeed * player.LastDirection, 0) * Time.deltaTime;
            if (dodgeCount % 4 == 0) player.CreateEcho();

            return;
        } else
        {
            player.SetState(new StateWalkJump(player));
            return;
        }
    }

    public override void OnStateEnter()
    {
        dodgeCount = player.dodgeCount;
        player.rb.velocity = Vector2.zero;
        player.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnStateExit()
    {
        player.rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }
}
