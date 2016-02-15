using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
public class Movement : MonoBehaviour {

    public UnitSelector selector;
    public Slider slider;
    public Button stopButton;
    public GameObject fireButton;
	void Start () {
        slider.onValueChanged.AddListener(a => { OnValueChange(); });
        stopButton.onClick.AddListener(()=>Stop());
        selector.OnCollectionChange += () => 
        {
            slider.transform.parent.gameObject.SetActive(selector.selectedUnits.Count != 0);
            stopButton.gameObject.SetActive(slider.gameObject.activeInHierarchy);
            fireButton.SetActive(selector.selectedUnits.Any(a => a.GetComponent<UnitController>() is IShootAttack)); 
        };
	}
	
	// Update is called once per frame
    public void FocusedTraejctory()
    {
        Debug.Log("Focus fire!!!!!!!!!");
        for (int i = 0; i < selector.selectedUnits.Count; i++)
        {
            ShootUnitController unit = selector.selectedUnits[i].GetComponent<ShootUnitController>();
            if (unit != null)
            {
                unit.ShowTraejctory();
            }
        }
    }
    public void Fire()
    {
        Debug.Log("fire!!!!!!!!!");
        for (int i = 0; i < selector.selectedUnits.Count; i++)
        {
            ShootUnitController unit = selector.selectedUnits[i].GetComponent<ShootUnitController>();
            if (unit != null)
            {
                unit.Shoot();
            }
        }
    }
    public void Stop()
    {
        foreach (var unit in selector.selectedUnits)
        {
            unit.GetComponent<UnitController>().Stop();
        }
    }
    void OnValueChange()
    {
        for (int i = 0; i < selector.selectedUnits.Count; i++)
			{
                UnitController unit = selector.selectedUnits[i].GetComponent<UnitController>();
                if (slider.value < 0)
                {
                    unit.course = Vector3.left;
                    if (unit.transform.rotation != Quaternion.Euler(0, 180, 0))
                    {
                        unit.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
                else if (slider.value<0.1&&slider.value>-0.1)
                {
                    slider.value = 0;
                    unit.Stop();
                }
                else
                {
                    unit.course = Vector3.right;
                    if (unit.transform.rotation != Quaternion.Euler(0, 0, 0))
                    {
                        unit.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                unit.currentSpeed =  unit.unit.speed * Mathf.Abs(slider.value);
			}
        
    }
}
