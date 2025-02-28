using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = InstanceManager.Instance.Get(InstanceType.Player);
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            target = InstanceManager.Instance.Get(InstanceType.Player);
            return;
        }
        
        transform.position =
            new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}