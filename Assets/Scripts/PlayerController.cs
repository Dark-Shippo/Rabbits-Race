using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private CharacterController controller;
    private Vector3 move;
    [SerializeField] private float interpolationSpeed = 5f;
    [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private float speedMin = 5f;
    [SerializeField] private float speedMax = 15f;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float gravityValue = -9.81f;
    public bool isOnGround;
    private Vector3 playerVelocity;
    public bool isDead = false;
    [SerializeField] private GameObject attackMesh;
    [SerializeField] private Menu menu;
    private bool isAttacking = false;
    [SerializeField] private Animator playerAnim;
    public int coins;
    [SerializeField] private TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        isOnGround = controller.isGrounded;
        move = new Vector3(xInput, 0, yInput).normalized;
        float attack = Input.GetAxisRaw("Fire1");
        float jump = Input.GetAxisRaw("Jump");
        
        if (!menu.menuOpen)
        {
            if (attack == 1 && !isAttacking)
            {
                StartCoroutine(Attacking());
            }

            if (isOnGround && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (xInput == 1 || xInput == -1 || yInput == 1 || yInput == -1)
            {
                playerAnim.SetBool("Walk", true);
            }
            else if (xInput == 0 && yInput == 0)
            {
                playerAnim.SetBool("Walk", false);
            }

            if (move != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(move, transform.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * interpolationSpeed);
                speed += speedIncrease * Time.deltaTime;
            }

            if (move == Vector3.zero)
            {
                speed = 5.0f;
            }

            //This is supposed to make the player jump while retaining any momentum gained
            if (jump == 1 && isOnGround)
            {
                playerVelocity.y = jumpSpeed;
            }
            //Applying Gravity to the Jump
            playerVelocity.y += gravityValue * Time.deltaTime;

            speed = Mathf.Clamp(speed, speedMin, speedMax);
            controller.Move((move * speed * Time.deltaTime) + (playerVelocity.y * Vector3.up * Time.deltaTime));
        }
    }
    
    IEnumerator Attacking()
    {
        isAttacking = true;
        attackMesh.SetActive(true);
        playerAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(3.5f);
        attackMesh.SetActive(false);
        playerAnim.SetTrigger("NotAttack");
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isAttacking)
        {
            if (!other.GetComponent<EnemyMovement>().isDead)
            {
                isDead = true;
                playerAnim.SetTrigger("DieT");
                playerAnim.SetBool("Die", true);
            }
        }
        if (other.CompareTag("Pit") || (other.CompareTag("Chest") && !isAttacking))
        {
            isDead = true;
            playerAnim.SetTrigger("DieT");
            playerAnim.SetBool("Die", true);
        }
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coins++;
            text.text = coins.ToString();
        }
        if (other.CompareTag("Portal1"))
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
        if (other.CompareTag("HUB"))
        {
            SceneManager.LoadScene("HUB", LoadSceneMode.Single);
        }
        
    }
}
