using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float JumpForce = 5f;

    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;

    private float _groundedRadius = .2f;

    private Rigidbody2D _rigidbody2D;
    private bool _isJumping;
    private float _input;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _isJumping = false;
        _isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        _input = Input.GetAxisRaw("Horizontal");
        //Potrei anche usare un vettore normalizzato

        Flip(_input);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        //Controllo che il Player non sia in aria
        _isGrounded = false;
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundedRadius, whatIsGround);

        _rigidbody2D.velocity = new Vector2(_input * speed, _rigidbody2D.velocity.y);

        if (_isJumping)
        {
            _rigidbody2D.AddForce(transform.up * (JumpForce), ForceMode2D.Impulse);
            _isJumping = false;
        }
    }

    private void Flip(float input)
    {
        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
