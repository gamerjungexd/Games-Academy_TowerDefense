using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private int damageToPlayer = 1;
    [SerializeField] private float toleranceDistance = 0.1f;

    [SerializeField] private GameObject[] body = null;

    private int waypointIndex = 0;
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    private WaveManager waveManager = null;
    private Player player;
    private CharacterController characterController = null;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (((Vector2)nextPosition - (Vector2)transform.position).magnitude <= toleranceDistance)
        {

            waypointIndex++;
            if (waveManager.NextPosition(waypointIndex, out Vector3 newPosition, out Quaternion newRotation))
            {
                if (player != null)
                {
                    player.OnDecreaseHealth(damageToPlayer);
                }

                waveManager.DecreaseUnitCount();
                Destroy(gameObject);
                return;
            }

            RotateBody(nextRotation);

            nextPosition = newPosition;
            nextRotation = newRotation;
        }

        Vector2 direction = ((Vector2)nextPosition - (Vector2)transform.position).normalized * Time.fixedDeltaTime;
        characterController.Move(direction);
    }

    public void InitalizeUnit(WaveManager waveManager, Player player, Vector3 nextPosition, Quaternion nextRotation, Quaternion spawnRotation)
    {
        this.waveManager = waveManager;
        this.player = player;
        this.nextPosition = nextPosition;
        this.nextRotation = nextRotation;

        RotateBody(spawnRotation);
    }

    private void RotateBody(Quaternion rotation)
    {
        foreach (GameObject obj in body)
        {
            obj.transform.rotation = rotation;
        }
    }

}
