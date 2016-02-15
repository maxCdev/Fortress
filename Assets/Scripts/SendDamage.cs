using UnityEngine;
using System.Collections;

public class SendDamage : MonoBehaviour {

	// Use this for initialization
    void Send()
    {
       transform.parent.SendMessage("HitAttack");
    }
    void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
