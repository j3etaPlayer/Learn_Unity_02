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
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (targetPoint.transform.position - transform.position).normalized;
        enemyRigid.AddForce(targetDirection * enemyMoveSpeed, forceMode);

        // acceleration : 지속적인 힘, 무게적용 X
        // force : 지속적인 힘, 무게적용 o
        // velocityChange : 순간적인 힘, 무게적용 X
        // impulse : 순간적인 힘, 무게적용 o
    }
}
