using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class AddUnit : MonoBehaviour {

   
    public UnitSpawner spawner;
 

    public void AddUnit1()
    {
        Unit unit=new Unit(Resources.Load<Sprite>("Sprite/unit1"));
        Add(unit);

    }
    public void AddUnit2()
    {
        Unit unit = new Unit(Resources.Load<Sprite>("Sprite/unit2"),1.3f,6,5,15,4,60,10,"Unit 2");
        Add(unit);

    }
    public void AddUnit3()
    {
        Unit unit = new Unit(Resources.Load<Sprite>("Sprite/unit3"),1.5f, 2, 2, 10, 1, 20, 4, "Unit 3");
        Add(unit);

    }
    public void AddUnit4()
    {
        Unit unit = new Unit(Resources.Load<Sprite>("Sprite/unit4"), 1.5f, 2, 2, 10, 1, 20, 4, "Unit 4");
        Add(unit);

    }
    public void AddUnit5Shoot()
    {
        Unit unit = new Unit(Resources.Load<Sprite>("Sprite/unit5"), 1.5f, 2, 2, 10, 1, 20, 2, "Unit 5");
        Add(unit);

    }
    public void Add(Unit unit)
    {
       spawner.AddToQueue(unit);
    }
}
