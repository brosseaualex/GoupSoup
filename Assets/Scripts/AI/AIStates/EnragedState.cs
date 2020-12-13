using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnragedState : BaseState
{
    public EnragedState(Monster _monster) : base("Enraged", _monster)
    {
        
    }

    public override void EnterState()
    {
        monster.InitializeMonsterColor(new Ingredient(255f, 0f, 0f));
        monster.aiInfo.aggroRange = 50;
        monster.aiInfo.attentionSpan = 10;
        monster.bodyInfo.maxSpeed = 20;
        monster.MoveTowards(PlayerManager.Instance.player.transform.position);
        base.EnterState();
    }

    public override void StayState(float dt)
    {
        base.StayState(dt);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
