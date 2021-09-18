using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDestroyTurret : MonoBehaviour
{
    [SerializeField] private float refundMultiply = 0.3f;

    private UserInput userInput = null;
    private Player player = null;
    void Awake()
    {
        userInput = FindObjectOfType<UserInput>();
        player = FindObjectOfType<Player>();

        gameObject.GetComponent<Button>().onClick.AddListener(DestroyTurret);
    }

    public void DestroyTurret()
    {
        player.EditResources((int)(userInput.LastClickedTurret.GetComponent<Turret>().Cost * refundMultiply));
        userInput.RemoveTurret(userInput.LastClickedTurret);
        Destroy(userInput.LastClickedTurret);
    }
}
