using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = null;

    [SerializeField] private float toleranceDistance = 0.1f;

    private int waypointIndex = 0;
    private Vector3 nextPosition;
    private CharacterController characterController = null;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        nextPosition = waypoints[0].position;
    }

    void FixedUpdate()
    {
        if (((Vector2)nextPosition - (Vector2)transform.position).magnitude <= toleranceDistance)
        {
            transform.rotation = waypoints[waypointIndex].rotation;

            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                //TODO Player HP abziehen
                Destroy(gameObject);
                return;
            }
            nextPosition = waypoints[waypointIndex].position;
        }

        Vector2 direction = ((Vector2)nextPosition - (Vector2)transform.position).normalized * Time.fixedDeltaTime;
        characterController.Move(direction);
    }

}
