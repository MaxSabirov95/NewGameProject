using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState
{
    public Player player;

    public MoveState(Player p)
    {
        player = p;
    }

   public virtual void OnEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }

}
