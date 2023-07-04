using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float jumpForce = 14;
    [SerializeField] private float friction = 0.6f;
    [SerializeField] private float maxSpeed = 5;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource attackAudio;

    private float _coyoteTime = 0.2f;// чем выше это значение тем дольше игрок сможет прыгать после отрыва от земли
    private float _jumpBufferTime = 0.2f;// сколько действует буффер 
    private float _coyoteTimeCounter;// счетчик времени кайота
    private float _jumpBufferCounter;// счетчик времени буфера
    private float _jumpFramesTimer;// доп проверка от багов, что бы не могли прыгать бесконечно

    private float _xAxis;
    private bool _grounded;

    public static PlayerMove Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _jumpFramesTimer += Time.deltaTime;

        _xAxis = Input.GetAxisRaw("Horizontal");

        if (_grounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _jumpFramesTimer > 0.4f)
        {
            _jumpFramesTimer = 0f;
            _jumpBufferCounter = 0f;
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space) && playerRigidbody.velocity.x > 0f)
        {
            _coyoteTimeCounter = 0f;
        }

        animator.SetBool("Walk", Input.GetAxis("Horizontal") != 0);


        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attackAudio.Play();
                animator.SetTrigger("Attack");
            }
        }
    }

    public void Jump()
    {
        jumpAudio.Play();
        animator.SetTrigger("Jump");
        playerRigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        float speedMultiplierInAir = 1f;
        float input = Input.GetAxis("Horizontal");

        //что бы трение действовало только на земле
        if (_grounded)
        {
            playerRigidbody.AddForce(input * moveSpeed, 0, 0, ForceMode.VelocityChange);
        }
        else// управляемый полет 
        {
            speedMultiplierInAir = 0.2f;
            // ограничеваем максимальную скорость в полете
            if (playerRigidbody.velocity.x > maxSpeed && input > 0)
            {
                speedMultiplierInAir = 0;
            }
            if (playerRigidbody.velocity.x < -maxSpeed && input < 0)
            {
                speedMultiplierInAir = 0;
            }
            playerRigidbody.AddForce(input * moveSpeed * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
        }
        // трение по оси х, что бы не ускоряться бесконечно
        playerRigidbody.AddForce(-playerRigidbody.velocity.x * friction * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision collision)
    {
        // определяем угол, что бы не прыгать об стены, смотрим точку контакта с поверхностями и куда она направлена
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            // если угол на котором мы стоим меньше 45 значит мы на земле, а если большето уже на горе
            if (angle < 45f)
            {
                _grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
