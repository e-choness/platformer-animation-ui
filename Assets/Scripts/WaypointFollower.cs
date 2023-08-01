using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints;
    [SerializeField] private float movingSpeed = 2.0f;

    private int _index = 0;
    // Update is called once per frame
    private void Update()
    {
        MovingBetween();
    }

    private void MovingBetween()
    {
        if (Vector2.Distance(waypoints[_index].transform.position, transform.position) < 0.1f)
        {
            _index ++;
            if (_index >= waypoints.Count)
            {
                _index = 0;
            }
        };
        transform.position = Vector2.MoveTowards(transform.position, 
            waypoints[_index].transform.position, 
            movingSpeed * Time.deltaTime);
    }
}
