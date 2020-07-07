using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalkJump : StateBase
{
    public StateWalkJump(Player player) : base(player)
    { }

    public override void Tick()
    {
        float movementX = player.input.horizontal * player.speed;
        player.anim.SetFloat("Moving", Mathf.Abs(movementX));
        player.UpdateLastDirection();
        player.FlipSprite();

        float movementY = player.rb.velocity.y;
        if (player.input.jump && player.JumpNumber > 0)
        {
            movementY = player.jumpSpeed;
            player.UseJump();
        }

        player.rb.velocity = new Vector2(movementX, movementY) * Time.deltaTime;

        if (player.input.shoot && player.ShootNumber > 0)
        {
            player.Shoot();
            player.UseShoot();
        }

        if (Mathf.Approximately(player.input.horizontal, 0) && Mathf.Approximately(player.rb.velocity.y, 0))
        {
            //POner animacion de quieto
            player.rb.velocity = Vector2.zero;
        }

        //StateChange
        if (player.input.dodge && player.DodgeNumber > 0)
        {
            player.UseDodge();
            player.SetState(new StateDodge(player));
        }
    }
}
