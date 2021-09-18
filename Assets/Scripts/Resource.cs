using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float delay = 2f;

    private int value = 0;
    private Player player = null;


    public void InitalizeResource(int initValue, Player player)
    {
        this.value = initValue;
        this.player = player;

        StartCoroutine(WaitForCollect());
    }

    private IEnumerator WaitForCollect()
    {
        yield return new WaitForSeconds(delay);

        player.EditResources(value);
        Destroy(gameObject);
    }
}
