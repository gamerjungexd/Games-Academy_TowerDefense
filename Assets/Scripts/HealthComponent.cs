using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private Slider healthbar = null;

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int dropResources = 1;

    [SerializeField] private GameObject resourceObject = null;

    private int health = 0;
    private WaveManager waveManager = null;
    private List<Turret> attackers = new List<Turret>();

    void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    public void OnDecreaseHealth(int value)
    {
        if (value > 0)
        {
            health -= value;
            if (health <= 0)
            {
                OnDeath();
                return;
            }
            UpdateHealthbar();
        }
    }

    private void UpdateHealthbar()
    {
        if (!healthbar.IsActive())
        {
            healthbar.gameObject.SetActive(true);
        }

        healthbar.value = (1f / (float)maxHealth) * (float)health;
    }

    private void OnDeath()
    {
        waveManager.DecreaseUnitCount();

        RemoveUnitFromTurretTarget();
        SpawnResources();

        Destroy(gameObject);
    }

    private void SpawnResources()
    {
        Resource temp = Instantiate<GameObject>(resourceObject, transform.position, transform.rotation).GetComponent<Resource>();
        temp.InitalizeResource(dropResources, waveManager.Player);
    }

    public void RemoveUnitFromTurretTarget()
    {
        foreach (Turret turret in attackers)
        {
            turret.RemoveTarget(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Turret turret = other.gameObject.GetComponent<Turret>();
        if (turret != null)
        {
            attackers.Add(turret);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Turret turret = other.gameObject.GetComponent<Turret>();
        if (turret != null)
        {
            attackers.Remove(turret);
        }
    }

}
