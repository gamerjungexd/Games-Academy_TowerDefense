using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCreateTurret : MonoBehaviour
{
    [SerializeField] private GameObject turretType = null;

    private UserInput userInput = null;
    private Player player = null;
    private Turret turret = null;

    void Awake()
    {
        userInput = FindObjectOfType<UserInput>();
        player = FindObjectOfType<Player>();
        turret = turretType.GetComponent<Turret>();

        gameObject.GetComponent<Button>().onClick.AddListener(CreateTurret);
    }

    public void CreateTurret()
    {
        int cost = turret.Cost;
        if (player.Resource >= cost)
        {
            player.EditResources(-cost);
            userInput.AddTurret(Instantiate<GameObject>(turretType, userInput.LastClickedCell, Quaternion.identity));
        }
    }

}
