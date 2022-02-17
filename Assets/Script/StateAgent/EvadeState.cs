using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State
{
    float preAngle;
    float preDistance;

    public EvadeState(StateAgent owner, string name) : base(owner, name)
    {

    }
    public override void OnEnter()
    {
        preAngle = owner.perception.angle;
        preDistance = owner.perception.distance;

        owner.perception.angle = 180;
        owner.perception.distance = 10;
        owner.movement.Resume();
    }

    public override void OnExit()
    {
        owner.perception.angle = preAngle;
        owner.perception.angle = preDistance;
    }

    public override void OnUpdate()
    {
        Vector3 direction = (owner.transform.position - owner.enemy.transform.position).normalized;
        owner.movement.MoveTowards(owner.transform.position + direction);
    }
}
