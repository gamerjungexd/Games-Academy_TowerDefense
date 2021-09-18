using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = null;
    [SerializeField] private Transform spawn = null;

    [SerializeField] private Wave[] waveList = null;

    private int unitCount = 0;
    private int waveIndex = 0;

    private Player player = null;
    void Start()
    {
        player = FindObjectOfType<Player>();

        StartCoroutine(SpawnWave(waveList[waveIndex]));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.content.Length; i++)
        {
            for (int j = 0; j < wave.content[i].count; j++)
            {
                yield return new WaitForSeconds(wave.content[i].delay);
                SpawnUnit(wave.content[i].unitType);
            }
            yield return new WaitForSeconds(wave.content[i].delayNextWaveContent);
        }
    }

    private void SpawnUnit(GameObject unitType)
    {
        GameObject unit = Instantiate(unitType, spawn.position, Quaternion.identity);
        unit.GetComponent<UnitMovement>().InitalizeUnit(this, player, waypoints[0].position, waypoints[0].rotation, spawn.rotation);
        unitCount++;
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

    public void DecreaseUnitCount()
    {
        unitCount--;

        if (unitCount <= 0)
        {
            waveIndex++;

            if (waveIndex >= waveList.Length)
            {
                Debug.Log("Win");
                return;
            }

            StartCoroutine(SpawnWave(waveList[waveIndex]));
        }
    }
}

