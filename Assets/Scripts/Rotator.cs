using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2.0f;
    // Start is called before the first frame update
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0.0f, 0.0f, 360.0f * rotationSpeed * Time.deltaTime);
    }
}
