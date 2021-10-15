using UnityEngine;

namespace OcelotDev
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float movementSpeed = 12f;
        private float horizontalMovement;
        private Rigidbody2D rB;

        [Header("Jumping")]
        public float jumpForce = 20f;
        private bool justJumped = false;

        [Header("Ground")]
        public bool onGround = false;
        public Collider2D floorCollider;
        public ContactFilter2D floorFilter;

        private void Start() => rB = GetComponent<Rigidbody2D>();
        
        private void Update()
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");

            onGround = floorCollider.IsTouching(floorFilter);
            
            
            if (!justJumped && Input.GetKeyDown(KeyCode.Space) && onGround)
                justJumped = true;
        }

        private void FixedUpdate()
        {
            rB.velocity = new Vector2(horizontalMovement * movementSpeed, rB.velocity.y);

            if(justJumped)
            {
                justJumped = false;
                rB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
