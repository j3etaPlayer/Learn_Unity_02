using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionable
{
    public void CollideWithPlayer(Transform player, float impectPower); // Player�� �ε��� ��ü�� Ư�� �������� ���󰡴� ����� �������̽����ϰڴ�.
}

public class SampleEnemy : MonoBehaviour, ICollisionable
{
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private ForceMode forceMode;
    public GameObject TargetPoint { get =>  targetPoint; set { targetPoint = value;} }
    private Vector3 targetDirection;
    private Rigidbody enemyRigid;


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

        // acceleration : �������� ��, �������� X
        // force : �������� ��, �������� o
        // velocityChange : �������� ��, �������� X
        // impulse : �������� ��, �������� o
    }

    private void OnTriggerEnter(Collider other)         // ��Ʈ���� ���� ���� ���� �����ϴ� �Լ�
    {
        if (other.gameObject.CompareTag("DestroyZone"))
        {
            Debug.Log($"destroy {other.name}");
            Destroy(gameObject);
        }
    }

    public void CollideWithPlayer(Transform player, float impectPower)
    {
        // �÷��̾�� �浹���� �� ��ü�� ���󰡴� ������ �ۼ�
        Vector3 awayVector = (transform.position - player.transform.position).normalized;  // ���� ���� - ����� ��ġ(player)
        enemyRigid.AddForce(awayVector * impectPower, ForceMode.Impulse);
    }




    //[SerializeField] private float impact = 10f;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("�浹!");

    //        Vector3 collidPlayer = (transform.position - collision.transform.position).normalized;

    //        enemyRigid.AddForce(collidPlayer * impact, ForceMode.Impulse);
    //    }
    //}
}
