using UnityEngine;
using System.Collections;

interface IHandAttack
{
    void Combat();
}
public class HandUnitController : UnitController,IHandAttack {

	// Use this for initialization

    protected override void Action(Transform unitHit, float distance)
    {
        base.Action(unitHit, distance);
    }


    public void Combat()
    {
        throw new System.NotImplementedException();
    }
}
