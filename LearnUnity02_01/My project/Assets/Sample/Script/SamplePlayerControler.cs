using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;           // 플레이어의 이동속도
    private Rigidbody playerRigid;                      // 플레이어의 물리 구현을 위한 컴포먼트

    [SerializeField] private GameObject powerIndicator; // 파워업 상태를 확인하기위한 오브젝트
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
            Invoke("PowerUpTimeOver", powerUpDuration);          // 뒤의 변수 : 파워업이 지속되길 원하는 시간 => 변수화 시켜서 데이터를 변경하거나 조합할 수 있다.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log($"{other.gameObject.name}");
            Destroy(other.gameObject);              // 오브젝트를 파괴한다.
            isPowerUp = true;                       // 파워업 상태인지 확인한다.
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
        //    Debug.Log("충돌!");
        //    Vector3 CollidEnemy = (collision.transform.position - transform.position).normalized;
        //    Rigidbody enemyrigid = collision.gameObject.GetComponent<Rigidbody>();
        //    enemyrigid.AddForce(CollidEnemy * impact, ForceMode.Impulse);
        //}
    }
}
