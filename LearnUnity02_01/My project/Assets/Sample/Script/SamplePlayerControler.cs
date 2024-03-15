using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;           // �÷��̾��� �̵��ӵ�
    private Rigidbody playerRigid;                      // �÷��̾��� ���� ������ ���� ������Ʈ

    [SerializeField] private GameObject powerIndicator; // �Ŀ��� ���¸� Ȯ���ϱ����� ������Ʈ
    public bool isPowerUp = false;
    [SerializeField] private float powerUpDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        playerRigid.AddForce(direction * moveSpeed);
        if (isPowerUp)
        {
            Invoke("PowerUpTimeOver", powerUpDuration);          // ���� ���� : �Ŀ����� ���ӵǱ� ���ϴ� �ð� => ����ȭ ���Ѽ� �����͸� �����ϰų� ������ �� �ִ�.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log($"{other.gameObject.name}");
            Destroy(other.gameObject);              // ������Ʈ�� �ı��Ѵ�.
            isPowerUp = true;                       // �Ŀ��� �������� Ȯ���Ѵ�.
            powerIndicator?.SetActive(true);
        }
    }

    private void PowerUpTimeOver()
    {
        isPowerUp = false;
        powerIndicator.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("�浹!");
        //    Vector3 CollidEnemy = (collision.transform.position - transform.position).normalized;
        //    Rigidbody enemyrigid = collision.gameObject.GetComponent<Rigidbody>();
        //    enemyrigid.AddForce(CollidEnemy * impact, ForceMode.Impulse);
        //}
    }
}
