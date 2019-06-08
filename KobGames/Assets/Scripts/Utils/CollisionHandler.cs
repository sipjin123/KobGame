using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public ObjectEvent CollidedObj = new ObjectEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            CollidedObj.Invoke(other.gameObject);
        }
    }
}