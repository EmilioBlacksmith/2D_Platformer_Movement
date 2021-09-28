 using UnityEngine;

namespace OcelotDev
{
    public class CharacterMovement : MonoBehaviour
    {
        public float horizontalMovement;
        public float movementSpeed = 2f;
        public bool onGround = false;
        public float jumpForce = 5f;

        [Header("Raycasting Ground")]
        public float groundLength = 0.6f;
        public Vector3 colliderOffset;
        public LayerMask groundLayer;

        [Header("The Motherfucker")]
        private Rigidbody2D rgb;

        private void Start() => rgb = GetComponent<Rigidbody2D>();

        public void FixedUpdate() => horizontalMovement = Input.GetAxisRaw("Horizontal"); 

        public void LateUpdate()
        {
            transform.Translate(new Vector3(horizontalMovement, 0f, 0f) * movementSpeed * Time.deltaTime);
            //rgb.MovePosition(transform.position + new Vector3(horizontalMovement, verticalMovement, 0f));

            onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) 
            || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

            if(Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                Jump();
            }      
        }

        void Jump()
        {
            rgb.velocity = new Vector2(rgb.velocity.x, 0);
            rgb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
            Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
        }
    }
}
