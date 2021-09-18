using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDestroyTurret : MonoBehaviour
{
    private UserInput userInput = null;
    void Awake()
    {
        userInput = FindObjectOfType<UserInput>();

        gameObject.GetComponent<Button>().onClick.AddListener(DestroyTurret);
    }

    public void DestroyTurret()
    {
        Destroy(userInput.LastClickedTurret);
    }
}
