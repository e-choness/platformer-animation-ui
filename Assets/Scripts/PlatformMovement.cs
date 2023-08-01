using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
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

    // Stick the player to the moving platform when standing on top of it. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
