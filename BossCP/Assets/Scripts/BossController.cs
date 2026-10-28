using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] BoxCollider2D hitbox;
    private SpriteRenderer sprite;
    private Vector2 startPos;
    private int health;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= 1.5f)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
        }
        else if (Vector2.Distance(playerTransform.position, transform.position) <= 5)
        {
            animator.SetBool("isRunning", true);
            Vector2 dir = playerTransform.position - transform.position;
            if (transform.rotation.y > 0)
            {
                dir.x *= -1;
            }
            transform.Translate(dir.normalized * Time.deltaTime * speed);
        }
        else if (transform.position.x - startPos.x < .1f && startPos.y - transform.position.y < .1f)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            Vector2 dir = new Vector2(startPos.x - transform.position.x, startPos.y - transform.position.y);
            if (transform.rotation.y > 0)
            {
                dir.x *= -1;
            }
            transform.Translate(dir.normalized * Time.deltaTime * speed);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void StartAttack()
    {
        hitbox.enabled = true;
    }

    public void StopAttack()
    {
        hitbox.enabled = false;
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void Damage()
    {
        health -= 1;
        sprite.color = Color.red;
        Invoke("ColorWhite", 0.2f);
    }

    private void ColorWhite()
    {
        sprite.color = Color.white;
    }
}
