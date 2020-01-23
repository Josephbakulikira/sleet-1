using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputData inputData;

    [SerializeField]
    private float m_speed = 5f;

    private Vector3 m_mousePos;
    private Vector3 m_releasePos;
    private Vector3 m_direction;

    Rigidbody2D m_rigidbody;
    Camera m_camera;
    Graphics player_Graphics;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_camera = FindObjectOfType<Camera>();
        player_Graphics = GetComponent<Graphics>();
    }
    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if(inputData.is_pressed)
        {
            m_mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            m_mousePos = new Vector3(m_mousePos.x, m_mousePos.y, 0f);

            //ResePlayerPos();
            player_Graphics.setDotStartPos(m_mousePos);
            player_Graphics.DotSwitcher(true);
        }

        if (inputData.is_held)
        {
            player_Graphics.SetDotPosition(m_mousePos, m_camera.ScreenToWorldPoint(Input.mousePosition));
            player_Graphics.MakePlayerPulse();
        }
        if (inputData.is_released)
        {
            m_releasePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            m_releasePos = new Vector3(m_releasePos.x, m_releasePos.y, 0f);

            player_Graphics.DotSwitcher(false);
            player_Graphics.ResetPlayerSize();
            CalculateDirection();
            MovePlayer();
        }
    }

    void CalculateDirection()
    {
        m_direction = (m_releasePos - m_mousePos).normalized;
    }
    void MovePlayer()
    {
        Vector3 opposite_direction = m_direction * -1;
        m_rigidbody.velocity = opposite_direction * m_speed;
    }

    void ResePlayerPos()
    {
        //spawn player at the mouse position
        transform.position = m_mousePos;
        m_rigidbody.velocity = Vector3.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 wall_normal = collision.contacts[0].normal;
            m_direction = Vector2.Reflect(m_rigidbody.velocity, wall_normal).normalized;

            m_rigidbody.velocity = m_direction * m_speed ;
        }
    }
}
