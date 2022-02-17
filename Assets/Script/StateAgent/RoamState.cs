using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    public RoamState(StateAgent owner, string name) : base(owner, name)
    {

    }

    public override void OnEnter()
    {
        Quaternion rotation = new Quaternion(0, Random.Range(-90f, 90f), 0, 0);//< create a quaternion with a random angle between - 90 and 90 and rotate about the y axis>;
        Vector3 forward = rotation * owner.transform.forward;//< set the forward vector by rotating the owner transform forward with the quaternion rotation >;
        Vector3 destination = owner.transform.position + (forward * Random.Range(10.0f, 15.0f));//< position of the owner + forward + random float between 10 and 15 >;

        owner.movement.MoveTowards(destination);
        owner.movement.Resume();
        owner.atDestination.value = false;
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if ((owner.transform.position - owner.movement.destination).magnitude <= 1.5)
        {
            owner.atDestination.value = true;
        }
    }
}

