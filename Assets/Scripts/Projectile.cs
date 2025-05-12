using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private float waitTime = 5.0f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private ChestAnim chest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (!chest.isDead)
        {
            Rigidbody clone;
            clone = Instantiate(projectile, enemy.transform.position, enemy.transform.rotation);
            clone.linearVelocity = transform.TransformDirection(Vector3.forward * speed);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
