using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
enum SweepCourse {None,Left,Right}
public class ScrollWorld : MonoBehaviour {
    Transform camerTransform;
    Vector2 lastPosition;
    Rigidbody rBody;
    bool isSweep = false;
    [SerializeField]
    float sensativity = 4;
	void Start () {
        rBody = GetComponent<Rigidbody>();
        camerTransform = transform;
	}
	void Update () {
	   if (Input.touchCount==1)
        {
           
            Touch touch=Input.touches[0];
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))//Проверяем не касаемся ли мы ui
            {
                return;
            }
           
            switch (touch.phase)
            {
                case TouchPhase.Began: {
                    lastPosition = touch.position;

                } break;
                case TouchPhase.Moved: {
                    isSweep = true;
                    if (touch.position.x > lastPosition.x)
                    {
                        rBody.AddForce(-camerTransform.right * sensativity, ForceMode.VelocityChange);
                    }
                    else if(touch.position.x < lastPosition.x)
                    {
                        rBody.AddForce(camerTransform.right * sensativity, ForceMode.VelocityChange);
                    }
                } break;
                case TouchPhase.Ended: {
                    isSweep = false;
                } break;
                case TouchPhase.Stationary:{
                    if (!isSweep)
                        rBody.velocity = Vector3.zero;         
                    }break;
            }
        }
	}

   
}
