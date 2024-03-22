using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;           // 플레이어의 이동속도
    private Rigidbody playerRigid;                      // 플레이어의 물리 구현을 위한 컴포먼트

    [SerializeField] private GameObject powerIndicator; // 파워업 상태를 확인하기위한 오브젝트
    public bool isPowerUp = false;
    [SerializeField] private float powerUpDuration = 5f;
    [SerializeField] private float impactPower = 10f;

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
            StartCoroutine(PlayerPolwerUp());
            // Invoke("PowerUpTimeOver", powerUpDuration);
            // 뒤의 변수 : 파워업이 지속되길 원하는 시간 => 변수화 시켜서 데이터를 변경하거나 조합할 수 있다.
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ICollisionable col = collision.gameObject.GetComponent<ICollisionable>();
        if (col != null)
        {
            Debug.Log("col 충돌!");
            col.CollideWithPlayer(transform, impactPower);
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
    IEnumerator PlayerPolwerUp()
    {
        isPowerUp = true;
        powerIndicator.SetActive(true);

        yield return new WaitForSeconds(powerUpDuration);
        
        isPowerUp = false;
        powerIndicator.SetActive(false);
    }

    //private void PowerUpTimeOver()
    //{
    //    isPowerUp = false;
    //    powerIndicator.SetActive(false);
    //}
}
