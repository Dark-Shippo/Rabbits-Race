using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] path;
    [SerializeField] private int currentPathIndex = 0;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float waitTime;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] private Animator turtleAnim;
    [SerializeField] private Collider turtleCollider;
    public bool isDead = false;
    [SerializeField] private bool isAttacking = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance && !isWaiting && !isDead && !isAttacking)
        {
            StartCoroutine(WaitThenMove());
        }
    }

    IEnumerator WaitThenMove()
    {
        isWaiting = true;
        turtleAnim.SetBool("Walk", false);
        yield return new WaitForSeconds(waitTime);
        if (!isDead)
        {
            MoveToDestination();
            turtleAnim.SetBool("Walk", true);
        }
        isWaiting = false;
    }

    private void MoveToDestination()
    {
        turtleAnim.SetBool("Walk", true);
            agent.SetDestination(path[currentPathIndex++].position);
            if (currentPathIndex >= path.Length)
            {
                currentPathIndex = 0;
            }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            agent.speed = 0;
            turtleAnim.SetTrigger("DeadT");
            turtleAnim.SetBool("Dead", true);
            GetComponent<CapsuleCollider>().enabled = false;
            isDead = true;
            StartCoroutine(HideTurtleCorpse());
        }
        else if (other.gameObject.CompareTag("Player") && !isDead)
        {
            agent.speed = 0;
            turtleAnim.SetBool("Walk", false);
            turtleAnim.SetTrigger("Attack1");
            isAttacking = true;
            turtleAnim.SetBool("Hide", true);
        }
    }

    IEnumerator HideTurtleCorpse()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
