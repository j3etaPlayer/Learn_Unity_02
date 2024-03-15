using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour
{
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private ForceMode forceMode;

    private Rigidbody enemyRigid;
    private Vector3 targetDirection;

    public GameObject TargetPoint { get =>  targetPoint; set { targetPoint = value;} }

    // Start is called before the first frame update
    void Start()
    {
        enemyRigid = GetComponent<Rigidbody>();
        targetPoint = GameObject.Find("CenterPivot");
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (targetPoint.transform.position - transform.position).normalized;
        enemyRigid.AddForce(targetDirection * enemyMoveSpeed, forceMode);

        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }

        // acceleration : 지속적인 힘, 무게적용 X
        // force : 지속적인 힘, 무게적용 o
        // velocityChange : 순간적인 힘, 무게적용 X
        // impulse : 순간적인 힘, 무게적용 o
    }
    [SerializeField] private float impact = 10f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌!");

            Vector3 collidPlayer = (transform.position - collision.transform.position).normalized;

            enemyRigid.AddForce(collidPlayer * impact, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);

            Debug.Log($"destroy {other.name}");
        }
    }
}
