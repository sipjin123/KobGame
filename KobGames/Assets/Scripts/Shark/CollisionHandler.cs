using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public ObjectEvent CollidedObj = new ObjectEvent();
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Kill();
            CollidedObj.Invoke(other.gameObject);
        }
    }
}
