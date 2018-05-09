using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    private float animationDuration = 3.0f;
    private float startTime;

    private bool isDead = false;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            return;
        }

        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity*Time.deltaTime;
        }

        //X left and right
        moveVector.x = Input.GetAxisRaw("Horizontal")*speed/2;
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                moveVector.x = speed/2;
            }
            else
            {
                moveVector.x = -speed/2;
            }
        }
        //Y up and down
        moveVector.y = verticalVelocity;
        //Z forward and backward
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
	}

    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    //Capsule(Player) hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if (hit.point.z > transform.position.z+controller.radius)
        if (hit.gameObject.tag == "Enemy" && hit.point.z > transform.position.z + controller.radius)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score>().onDeath();
    }
}
