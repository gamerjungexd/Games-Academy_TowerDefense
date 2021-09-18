using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform modelHead = null;

    [SerializeField] private int damage = 2;
    [SerializeField] private float attackSpeed = 1f;

    [SerializeField] private float effectTime = 0.5f;
    [SerializeField] private GameObject[] attackEffect = null;

    private int indexAttackEffect = 0;
    private List<GameObject> targets = new List<GameObject>();


    void Start()
    {
        StartCoroutine(ShotTarget());
    }

    void Update()
    {
        if (modelHead != null && targets.Count > 0)
        {
            modelHead.rotation = Quaternion.Euler(0f, 0f, Vector3.SignedAngle(Vector3.up, (targets[0].transform.position - modelHead.position), Vector3.forward));
        }
    }

    public IEnumerator ShotTarget()
    {
        yield return new WaitForSeconds(attackSpeed);

        yield return new WaitUntil(() => targets.Count > 0);

        OnAttack();

        StartCoroutine(ShotTarget());
    }

    public void OnAttack()
    {
        StartCoroutine(ShowAttack());
        HealthComponent component = targets[0].GetComponent<HealthComponent>();
        if (component != null)
        {
            component.OnDecreaseHealth(damage);
        }
    }

    private IEnumerator ShowAttack()
    {
        if (attackEffect.Length <= 0)
        {
            yield break;
        }

        attackEffect[indexAttackEffect].SetActive(true);
        yield return new WaitForSeconds(effectTime);
        attackEffect[indexAttackEffect].SetActive(false);

        if (indexAttackEffect < attackEffect.Length - 1)
        {
            indexAttackEffect++;
        }
        else if (attackEffect.Length > 1 && indexAttackEffect >= attackEffect.Length - 1)
        {
            indexAttackEffect = 0;
        }
    }

    public void RemoveTarget(GameObject obj)
    {
        targets.Remove(obj);
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
