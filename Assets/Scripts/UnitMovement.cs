using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float toleranceDistance = 0.1f;

    private int waypointIndex = 0;
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    private WaveManager waveManager = null;
    private CharacterController characterController = null;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (((Vector2)nextPosition - (Vector2)transform.position).magnitude <= toleranceDistance)
        {
            transform.rotation = nextRotation;

            waypointIndex++;
            if (waveManager.NextPosition(waypointIndex, out Vector3 newPosition, out Quaternion newRotation))
            {
                //TODO Player HP abziehen
                waveManager.DecreaseUnitCount();
                Destroy(gameObject);
                return;
            }
            
            nextPosition = newPosition;
            nextRotation = newRotation;
        }

        Vector2 direction = ((Vector2)nextPosition - (Vector2)transform.position).normalized * Time.fixedDeltaTime;
        characterController.Move(direction);
    }

    public void InitalizeUnit(WaveManager waveManager, Vector3 nextPosition, Quaternion nextRotation)
    {
        this.waveManager = waveManager;
        this.nextPosition = nextPosition;
        this.nextRotation = nextRotation;
    }

}
