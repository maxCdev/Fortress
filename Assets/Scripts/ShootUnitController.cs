using UnityEngine;
using System.Collections;

interface IShootAttack
{
    void Shoot();
}
public class ShootUnitController : UnitController,IShootAttack {

    float aimSpeed = 0.5f;
    public Transform body;
    bool aim = false;
    Quaternion newRot = Quaternion.identity;

    protected override void Action(Transform unitHit, float distance)
    {
        base.Action(unitHit, distance);
    }
    public void ShowTraejctory()
    {
        aim = true;
      
    }
    public void Shoot()
    {
        anim.Play("Attack");
        StartCoroutine(EndShoot(anim.GetCurrentAnimatorClipInfo(0).Length));
    }
    IEnumerator EndShoot(int animTime)
    {
        yield return new WaitForSeconds(animTime/2);
        aim = false;
    }
    void Update()
    {
        base.Update();
        if (aim)
        {
            newRot = Quaternion.Lerp(body.localRotation, Quaternion.Euler(-Vector3.forward * 50f), aimSpeed * Time.deltaTime);
        }
        else
        {
            newRot = Quaternion.Lerp(body.localRotation, Quaternion.identity, aimSpeed*10 * Time.deltaTime);
        }
    }
    void LateUpdate()
    {
            body.localRotation = newRot;
    }
}
