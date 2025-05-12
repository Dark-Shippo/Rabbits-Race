using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ChestAnim : MonoBehaviour
{
    [SerializeField] Animator chestAnim;
    [SerializeField] private float waitTime1 = 3.0f;
    [SerializeField] private float waitTime2 = 2.0f;
    [SerializeField] private bool isAttacking;
    public bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAttacking = false;
        StartCoroutine(ChestAttackAnim(isAttacking));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            isDead = true;
            chestAnim.SetTrigger("DieT");
            chestAnim.SetBool("Die", true);
        }
    }

    IEnumerator ChestAttackAnim(bool attack)
    {
        while (!isDead)
        {
            attack = true;
            chestAnim.SetBool("Attacking", true);
            yield return new WaitForSeconds(waitTime1);
            chestAnim.SetBool("Attacking", false);
            attack = false;
            yield return new WaitForSeconds(waitTime2);
            chestAnim.SetTrigger("AttackT");
        }
    }
}
