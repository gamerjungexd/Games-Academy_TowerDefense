using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCreateTurret : MonoBehaviour
{
    [SerializeField] private GameObject turretType = null;

    private UserInput userInput = null;

    void Awake()
    {
        userInput = FindObjectOfType<UserInput>();

        gameObject.GetComponent<Button>().onClick.AddListener(CreateTurret);
    }

    public void CreateTurret()
    {
        userInput.AddTurret(Instantiate<GameObject>(turretType, userInput.LastClickedCell, Quaternion.identity));
    }

}
