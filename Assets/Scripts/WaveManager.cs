using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = null;

    [SerializeField] private GameObject unitType = null;
    [SerializeField] private Transform spawn = null;

    [SerializeField] private float spawnDelay = 0.5f;

    void Start()
    {

        StartCoroutine(SpawnWave());

    }

    void Update()
    {

    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnUnit(unitType);
        }
    }

    private void SpawnUnit(GameObject unitType)
    {
        GameObject unit = Instantiate(unitType, spawn.position, spawn.rotation);
        unit.GetComponent<UnitMovement>().InitalizeUnit(this, waypoints[0].position, waypoints[0].rotation);
    }

    public bool NextPosition(int index, out Vector3 nextPosition, out Quaternion nextRotation)
    {
        if (index >= waypoints.Length)
        {
            nextPosition = Vector3.zero;
            nextRotation = Quaternion.identity;
            return true;
        }
        nextPosition = waypoints[index].position;
        nextRotation = waypoints[index].rotation;
        return false;
    }
}

