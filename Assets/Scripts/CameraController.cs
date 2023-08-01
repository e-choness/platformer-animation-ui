using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void Update()
    {
        var position = player.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
