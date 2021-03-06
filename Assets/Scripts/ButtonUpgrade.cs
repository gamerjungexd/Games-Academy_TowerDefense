using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUpgrade : MonoBehaviour
{
    private UserInput userInput = null;
    private WaveManager waveManager = null;

    void Awake()
    {
        userInput = FindObjectOfType<UserInput>();
        waveManager = FindObjectOfType<WaveManager>();

        gameObject.GetComponent<Button>().onClick.AddListener(UpgradeTurret);
    }

    public void UpgradeTurret()
    {
        Turret turret = userInput.LastClickedTurret.GetComponent<Turret>();
        GameObject upgradedTurret = waveManager.GetTurretUpgrade(turret.Type, turret.TypeLevel + 1);
        int cost = upgradedTurret.GetComponent<Turret>().Cost;

        if(waveManager.Player.Resource >= cost)
        {
            waveManager.Player.EditResources(-cost);
            userInput.AddTurret(Instantiate<GameObject>(upgradedTurret, turret.transform.position, turret.transform.rotation));
            userInput.RemoveTurret(turret.gameObject);

            Destroy(turret.gameObject);
        }
    }
}
