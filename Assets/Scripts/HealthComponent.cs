using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private Slider healthbar = null;

    [SerializeField] private int maxHealth = 10;

    private int health = 0;
    private List<Turret> attackers = new List<Turret>();

    void Awake()
    {
        health = maxHealth;
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
        RemoveUnitFromTurretTarget();
       
        Destroy(gameObject);
    }

    public void RemoveUnitFromTurretTarget()
    {
        foreach(Turret turret in attackers)
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
