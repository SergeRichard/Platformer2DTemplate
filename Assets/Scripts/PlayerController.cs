using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;

	public float jumpSpeed;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator myAnim;
	private SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (Input.GetAxisRaw ("Horizontal") > 0f) {
			myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
			mySpriteRenderer.flipX = false;
		} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
			myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
			mySpriteRenderer.flipX = true;
		} else {
			myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
		}
		if (Input.GetButtonDown ("Jump") && isGrounded) {
			myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpSpeed, 0f);
		}

		myAnim.SetFloat ("Speed", Mathf.Abs(myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);
	}
}
