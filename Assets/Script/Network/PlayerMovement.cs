using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;

    public float PlayerSpeed = 5f;

    public Camera Camera;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<FirstPersonCamera>().Target = transform;
        }
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        playerMovement();
        playerRotation();
    }

    void playerMovement()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("UpDown"), Input.GetAxis("Vertical")) 
            * Runner.DeltaTime * PlayerSpeed;

        //_controller.Move(moveVec);
        _controller.Move(transform.TransformDirection(moveVec));


        if (moveVec != Vector3.zero)
        {
            gameObject.transform.Translate(moveVec);
        }
    }
    void playerRotation()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        transform.rotation = Camera.transform.rotation;
    }
}