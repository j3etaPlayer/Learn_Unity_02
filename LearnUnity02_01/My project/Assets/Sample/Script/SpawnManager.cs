using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemySpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �Ѱ��� ���� Ư����ġ���� �����Ǵ� �ڵ�
    private void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(enemy, enemySpawnPosition.position, Quaternion.identity);
    }
}
