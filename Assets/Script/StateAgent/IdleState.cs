using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{   public IdleState(StateAgent owner, string name) : base(owner, name)
    {
        
    }
    public override void OnEnter()
    {
        Debug.Log(name + " enter");
    }

    public override void OnExit()
    {
        Debug.Log(name + " exit");

    }

    public override void OnUpdate()
    {
        Debug.Log(name + " update");
    }


}
