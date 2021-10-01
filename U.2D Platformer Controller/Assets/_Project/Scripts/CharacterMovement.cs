using UnityEngine;

namespace OcelotDev
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float movementSpeed = 12f;
        public float horizontalMovement;

        [Header("Gravity")]
        public bool onGround = false;
        public float JumpForce = 20f;
        private Rigidbody2D rgb;

        [Header("Raycast")]
        public float groundLength = 0.6f;
        public Vector3 colliderOffset;
        public LayerMask groundLayer;

        private void Start()
        {
            rgb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        private void LateUpdate()
        {
            transform.Translate(new Vector3(horizontalMovement, 0f, 0f) * movementSpeed * Time.deltaTime);

            onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer)
                || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

            if(Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                Jump();
            }
        }

        void Jump()
        {
            rgb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
            Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
        }
    }
}
