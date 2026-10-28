using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb;
    private Vector2 dir;
    [SerializeField] GameObject bossObject;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = dir.x * speed;
        rb.linearVelocityY = dir.y * speed;
    }

    public void Movement(InputAction.CallbackContext value)
    {
        dir = new Vector2(value.ReadValue<Vector2>().x, value.ReadValue<Vector2>().y);
    }

    public void Attack()
    {
        if (Vector2.Distance(bossObject.transform.position, transform.position) <= 2)
        {
            bossObject.GetComponent<BossController>().Damage();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.color = Color.red;
        Invoke("ColorWhite", 0.2f);


    }
    private void ColorWhite()
    {
        sprite.color = Color.white;
    }
}
