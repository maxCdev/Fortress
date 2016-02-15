using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
public class UnitSpawner : MonoBehaviour {
    Queue<Unit> unitsQueue = new Queue<Unit>();
    public Text buildProcentText;
    public Scrollbar buildScroll;
    public bool queueIsFree = true;
    public bool builderFree = true;
    public float buildCooficent = 1;
    public GameObject queueIcons;
    const int maxQueueCount = 8;
	void Start () {
        buildProcentText.text = "0";
	}

    public void AddToQueue(Unit unit)
    {
        if (unitsQueue.Count <= maxQueueCount)
        {
            unitsQueue.Enqueue(unit);
            UnitIconsUpdate();
        }
    }
    void CreateUnitIcon(Unit unit, Vector3 position)
    {
        Sprite icon = unit.sprite;
        GameObject image = new GameObject();
        image.AddComponent<RectTransform>();
        image.AddComponent<Image>();
        image.AddComponent<Button>();
        image.GetComponent<Image>().sprite = icon;
        image.GetComponent<Button>().onClick.AddListener(() => { DeleteFromQueue(unit); UnitIconsUpdate(); });
        image.transform.localScale = new Vector3(0.4f, 0.4f, 0); ;
        image.transform.SetParent(queueIcons.transform);
        image.transform.localPosition = position;
    }
    void UnitIconsUpdate()
    {
       List<Unit> units = unitsQueue.ToList();
       if (queueIcons.transform.childCount > 0)
       {
           for (int i = 0; i < queueIcons.transform.childCount; i++)
           {
               Destroy(queueIcons.transform.GetChild(i).gameObject);
           }
       }
        if (units.Count!=0)
       for (int i = unitsQueue.Count-1; i >= 0; i--)
            {
                if (queueIcons.transform.childCount == 0)
                {
                    CreateUnitIcon(units[i], Vector3.zero);
                }
                else 
                {
                    CreateUnitIcon(units[i], new Vector3(i * 100 + 10, 0, 0));
                }
            }
        
    }
    void DeleteFromQueue(Unit deleteUnit)
    {
      List<Unit> list =  unitsQueue.ToList();
      list.Remove(deleteUnit);
      unitsQueue=new Queue<Unit>(list);
    }
    void Update()
    {
        if (queueIsFree && builderFree && unitsQueue.Count > 0)
        {
            Unit unit = unitsQueue.Peek();
            builderFree = false;
            StartCoroutine(BuildWaiter(unit));
            
        }
    }
    IEnumerator BuildWaiter(Unit unit)
    {
        float procent;
        for (int i = 0; i < unit.buildTime; i++)
        {
            if (unitsQueue.Count==0||unitsQueue.Peek() != unit)
            {
                break;
            }
            procent = 100 / unit.buildTime * i;
            buildProcentText.text = procent + "% ";
            buildScroll.size = procent / 100;
            yield return new WaitForSeconds(buildCooficent);
        }
        if (unitsQueue.Count!=0&&unitsQueue.Peek() == unit)
        {
            unitsQueue.Dequeue();
            UnitIconsUpdate();
            Builder(unit);
        }
        buildProcentText.text = "0";
        buildScroll.size = 0;
        builderFree = true;
    }
    void Builder(Unit unit)
    {
        GameObject unitSpawn = Resources.Load<GameObject>(unit.name);
        UnitController unitContrl = unitSpawn.GetComponent<UnitController>();
        Quaternion rotateUnit = new Quaternion();
        if (transform.parent.tag == "Tower1")
        {
            unitContrl.course = Vector3.right;
        }
        else
        {
            rotateUnit = Quaternion.Euler(0, 180, 0);//delete its 
            unitContrl.course = Vector3.left;
        }
        unitContrl.unit = unit;
        unitSpawn = Instantiate(unitSpawn, transform.position, rotateUnit) as GameObject;
        unitSpawn.transform.parent = transform.parent;
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Unit")
        {
            queueIsFree = false;
        }
        else
        {
            queueIsFree = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Unit")
        {
            queueIsFree = false;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Unit")
        {
            queueIsFree = true;
        }

    }
}
