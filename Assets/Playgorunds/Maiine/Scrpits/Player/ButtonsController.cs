using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController
{
    Player _player;
    Vector3 _movementVector;
    Vector3 _cameraVector;
    float mouseX;
    float mouseY;

    public ButtonsController(Player player)
    {
        _player = player;
    }

    public void OnUpdate()
    {
        MovementInputs();
        CameraInputs();
        JumpInput();
    }

    void MovementInputs()
    {
        float horizontalK = Input.GetAxisRaw("HorizontalK");
        float verticalK = Input.GetAxisRaw("VerticalK");

        float horizontalJ = Input.GetAxis("HorizontalJ");
        float verticalJ = Input.GetAxis("VerticalJ");

        float horizontal = horizontalK + horizontalJ;
        float vertical = verticalK + verticalJ;

        _player.Movement(horizontal, vertical);
    }

    void CameraInputs()
    {
        float axieX = Input.GetAxis("Mouse X") + Input.GetAxis("JoyAxis X") * -1;
        float axieY = Input.GetAxis("Mouse Y") + Input.GetAxis("JoyAxis Y") * 0.5f;

        Mathf.Clamp01(axieX);
        Mathf.Clamp01(axieY);

        mouseX += axieX * Time.deltaTime;
        mouseY += axieY * Time.deltaTime * -1;

        _cameraVector = new Vector3(mouseY, mouseX, 0);

        _player.CameraController(_cameraVector);
    }

    void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
            _player.Jump();
    }

    void AimInput()
    {

    }

    void ShootInput()
    {

    }

    void InteractInput()
    {

    }
}
