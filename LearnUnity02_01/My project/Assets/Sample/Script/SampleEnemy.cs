using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionable
{
    public void CollideWithPlayer(Transform player, float impectPower); // Player과 부딪힌 객체가 특정 방향으로 날라가는 기능을 인터페이스ㄹ하겠다.
}

public class SampleEnemy : MonoBehaviour, ICollisionable
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

    public void CollideWithPlayer(Transform player, float impectPower)
    {
        // 플레이어와 충돌했을 때 객체가 날라가는 로직을 작성
        Vector3 awayVector = (transform.position - player.transform.position).normalized;  // 날라갈 방향 - 출발할 위치(player)

        enemyRigid.AddForce(awayVector * impectPower, ForceMode.Impulse);
    }
}
