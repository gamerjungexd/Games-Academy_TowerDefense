using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject gameWinScreen = null;
    [SerializeField] private Transform[] waypoints = null;
    [SerializeField] private Transform spawn = null;

    [SerializeField] private Wave[] waveList = null;

    [SerializeField] private TMP_Text textfieldWaveIndex = null;

    [SerializeField] private GameObject[] turretUpgrades = null;

    private int unitCount = 0;
    private int waveIndex = 0;

    private Dictionary<TurretType, int> highestTurretLevel = new Dictionary<TurretType, int>();
    public Dictionary<TurretType, int> HighestTurretLevel { get => this.highestTurretLevel; }

    private Player player = null;
    void Start()
    {
        player = FindObjectOfType<Player>();

        SetUIWaveIndex();
        InitHighestUpgradeLevel();

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
                Time.timeScale = 0f;
                gameWinScreen.SetActive(true);
                return;
            }

            SetUIWaveIndex();
            StartCoroutine(SpawnWave(waveList[waveIndex]));
        }
    }

    private void SetUIWaveIndex()
    {
        if (textfieldWaveIndex == null)
        {
            return;
        }

        textfieldWaveIndex.text = "" + (waveIndex + 1);
    }

    public GameObject GetTurretUpgrade(TurretType type, int value)
    {
        foreach (GameObject upgrades in turretUpgrades)
        {
            Turret turret = upgrades.GetComponent<Turret>();
            if (turret.Type == type && turret.TypeLevel == value)
            {
                return upgrades;
            }
        }

        return null;
    }

    private void InitHighestUpgradeLevel()
    {
        foreach (GameObject upgrades in turretUpgrades)
        {
            Turret turret = upgrades.GetComponent<Turret>();
            if (!highestTurretLevel.ContainsKey(turret.Type))
            {
                highestTurretLevel.Add(turret.Type, turret.TypeLevel);
            }
            else
            {
                if (highestTurretLevel[turret.Type] < turret.TypeLevel)
                {
                    highestTurretLevel[turret.Type] = turret.TypeLevel;
                }
            }
        }
    }
}

