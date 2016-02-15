using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
public class UnitSelector : MonoBehaviour {

    public List<Transform> selectedUnits=new List<Transform>();
    public event UnityAction OnCollectionChange=null;
    public bool addSelectButtonDown = true;

    void SelectOne(Transform unit)
    {
        selectedUnits.Clear();
        selectedUnits.Add(unit);
    }
    void TrySelect(Transform unit)
    {
        if (addSelectButtonDown)
        {
            if (selectedUnits.Contains(unit))
            {
                RemoveSelect(unit);
            }
            else
            {
                AddSelect(unit);
            }
        }
        else
        {
            SelectOne(unit);
        }
        if (OnCollectionChange != null)
        {
            OnCollectionChange();
        }
    }
    public void AddButton()
    {
        addSelectButtonDown = !addSelectButtonDown;
    }
    void AddSelect(Transform unit)
    {
        selectedUnits.Add(unit);
    }
    void RemoveSelect(Transform unit)
    {
        selectedUnits.Remove(unit);
    }
    void SelectMany()
    {
        //выделение линией
    }
      
}
