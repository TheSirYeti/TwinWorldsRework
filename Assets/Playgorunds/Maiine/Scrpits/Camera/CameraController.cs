using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAtCamera;

    public Transform basePositionCam;
    public Transform aimPositionCam;

    public float minDistance;
    public float cameraSpeed;
    public float cameraAimSpeed;

    delegate void CameraMovement();
    CameraMovement actualMovemenmt = delegate { };

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        transform.LookAt(lookAtCamera);
        actualMovemenmt();

        if (Input.GetMouseButtonDown(1))
        {
            actualMovemenmt = AimCam;
        }
        if (Input.GetMouseButtonUp(1))
        {
            actualMovemenmt = ThirdPersonCam;
        }
    }

    public void UpdateMovement(int dir)
    {
        if (dir == 0)
            actualMovemenmt = GoForward;
        else if (dir == 1)
            actualMovemenmt = GoBackward;
        else if (dir == 2)
            actualMovemenmt = delegate { };
    }

    public void AimCam()
    {
        transform.position += (aimPositionCam.position - transform.position) * Time.deltaTime * cameraAimSpeed;

        if (Vector3.Distance(aimPositionCam.position, transform.position) < minDistance)
            actualMovemenmt = delegate { };
    }

    public void ThirdPersonCam()
    {
        transform.position += (basePositionCam.position - transform.position) * Time.deltaTime * cameraAimSpeed;

        if (Vector3.Distance(basePositionCam.position, transform.position) < minDistance)
            actualMovemenmt = delegate { };
    }

    public void GoForward()
    {
        if (transform.localPosition.z < -1.5f)
        {
            transform.position += transform.forward * cameraSpeed * Time.deltaTime;
        }
    }

    public void GoBackward()
    {
        if (transform.localPosition.z > -7.5f)
            transform.position += transform.forward * -1 * cameraSpeed * Time.deltaTime;
    }
}
