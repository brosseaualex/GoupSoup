using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster {

    //No need for inheritance in this case, it just exists in case the children have unique behaviours
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void UpdateMonster(float dt)
    {
        base.UpdateMonster(dt);
    }
}
