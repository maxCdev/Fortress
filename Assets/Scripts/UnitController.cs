using UnityEngine;
using System.Collections;

[System.Serializable]
public class Unit
{
    public string name;
    public float speed;
    public float attack;
    public float armor;
    public float hp;
    public float attackSpeed;
    public float price;
    public float buildTime;
    public Sprite sprite;
    public Unit(Sprite sprite,
                float speed=1,
                float attack=5,
                float armor=3,
                float hp=15,
                float attackSpeed=2,
                float price=30,
                float buildTime=5,
                string name="Unit")
    {
        this.sprite = sprite;
        this.speed = speed;
        this.attack = attack;
        this.armor = armor;
        this.hp = hp;
        this.attackSpeed = attackSpeed;
        this.price = price;
        this.buildTime = buildTime;
        this.name = name;
    }
}
enum CombatStates {Attack,Block,Damage}
public class UnitController : MonoBehaviour {
    public float combatDisatance=0.3f;
    public float stopDistance=0.3f;
    public Unit unit;
    public Vector3 course;
    private Transform unitTransform;
    private bool stop=false;
    private float lastAtack = 0;
    protected Animator anim;
    public float currentSpeed;
    Transform currentEnemie;//delete this
    bool isDie = false;
    bool IsDie {
        set { anim.SetTrigger("Die"); isDie = value; }
        get{return isDie;}
    }
	void Start () {
        currentSpeed = unit.speed;
        unitTransform = transform;
        anim = GetComponentInChildren<Animator>();
	}
    void FixedUpdate()
    {
        if (!stop)
        {
            anim.SetFloat("State", 1);
            unitTransform.position = Vector3.Lerp(unitTransform.position, unitTransform.position + course, currentSpeed * Time.deltaTime);
        }
    }
    public void Stop()
    {
        anim.SetFloat("State", 0);
        stop = true;
    }
	// Update is called once per frame
    virtual protected void Action(Transform unitHit, float distance)
    {
        if (distance <= combatDisatance && unitHit.parent.name != unitTransform.parent.name)//если впереди враг
        {
            //Combat(unitHit);
        }
        else if (distance <= stopDistance && unitHit.parent.name == unitTransform.parent.name)//если впереди друг
        {           
            Stop();
        }
        else
        {
            stop = false;
        }
    }
    // Нанесение урона этому юниту
    void AddDamage(float damage)
    {
        if (IsDie)
        {
            return;
        }
        if (damage <= unit.armor)
        {
            damage = unit.armor + 1;
        }
        if (damage > unit.armor + unit.hp)
        {
            IsDie = true;
        }
        else
        {
            anim.SetTrigger("Damage");
            unit.hp -= damage - unit.armor;
            lastAtack += anim.GetCurrentAnimatorStateInfo(0).length;
        }
    }
    //void HitAttack()
    //{
    //    if (currentEnemie != null)
    //    {
    //        currentEnemie.SendMessage("AddDamage", unit.attack, SendMessageOptions.DontRequireReceiver);
    //    }
    //}
    //bool Attack(Transform enemie)
    //{
    //    if (lastAtack+unit.attackSpeed< Time.time&&anim.GetFloat("State")==2f)
    //    {
    //        anim.SetTrigger("Attack");
    //        lastAtack = Time.time;
    //        currentEnemie = enemie;
    //        return true;
    //    }
    //    return false;
    //}
    //void Block()
    //{ }
    //void Combat(Transform enemie)
    //{
    //    anim.SetFloat("State", 2);
    //    if (!Attack(enemie))
    //    {
    //        //логика блока
    //    }
    //    Stop();
    //}
    void DetectWhoIsNext()
    {
        RaycastHit hit;
        Ray ray = new Ray(unitTransform.position, course);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit, 90f))
        {
            if (hit.collider.tag == "Unit")
            {
                Action(hit.collider.transform, hit.distance);
            }
            else
            {
                stop = false;
            }
        }
       
        
    }
	protected void Update () {
        DetectWhoIsNext();
	}
    //не вошел ли юнит башню
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tower1" || other.tag == "Tower2")
        {
            if (other.tag != unitTransform.parent.tag)
            {
                PackToBuffer();
            }
         }
      
    }
    public void OnMouseDown()
    {
        unitTransform.parent.SendMessage("TrySelect", unitTransform);
    }
    void PackToBuffer()
    {
        unitTransform.parent = GameObject.Find("Buffer").transform;
        unitTransform.localPosition = Vector3.zero;
        unitTransform.GetComponentInChildren<Renderer>().enabled = false;
    }

}
