using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Turret : MonoBehaviour
{

    [SerializeField] private TurretType type = 0;
    public TurretType Type { get => this.type; }

    [SerializeField] private int typeLevel = 0;
    public int TypeLevel { get => this.typeLevel; }

    [SerializeField] private int cost = 1;
    public int Cost { get => this.cost; }

    [Min(0)]
    [SerializeField] private int damage = 2;

    [Tooltip("How long should the turret wait between the attacks in seconds.\n[Min 0f]")]
    [Min(0f)]
    [SerializeField] private float attackSpeed = 1f;

    [Space(10f)]
    [SerializeField] private float effectTime = 0.5f;
    [SerializeField] private GameObject[] attackEffect = null;

    [Header("Model:")]
    [SerializeField] private Transform modelHead = null;

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
        if (targets[0] != null)
        {
            HealthComponent component = targets[0].GetComponent<HealthComponent>();
            if (component != null)
            {
                component.OnDecreaseHealth(damage);
            }
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
