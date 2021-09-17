using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform modelHead = null;

    private List<GameObject> targets = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        if (modelHead != null && targets.Count > 0)
        {
            modelHead.rotation = Quaternion.Euler(0f, 0f, Vector3.SignedAngle(Vector3.up, (targets[0].transform.position - modelHead.position), Vector3.forward));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        targets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
    }
}
