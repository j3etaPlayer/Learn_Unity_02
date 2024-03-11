using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject myObject;

    private Rigidbody playerRigid;

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
    }
}
