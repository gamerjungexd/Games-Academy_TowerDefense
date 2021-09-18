using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private TMP_Text textfieldHealth = null;
    [SerializeField] private TMP_Text textfieldResources = null;

    [SerializeField] private int startResources = 10;

    private int health = 0;
    private int resources = 0;
    public int Resource { get => this.resources; }

    void Start()
    {
        health = maxHealth;
        resources = startResources;

        UpdateHealthbar();
        UpdateUIResource();
    }

    public void OnDecreaseHealth(int value)
    {
        health -= value;
        if (health <= 0)
        {
            OnDeath();
            return;
        }
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        textfieldHealth.text = "" + health;
    }
    private void UpdateUIResource()
    {
        textfieldResources.text = "" + resources;
    }

    private void OnDeath()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void EditResources(int value)
    {
        resources += value;
        UpdateUIResource();
    }
}
