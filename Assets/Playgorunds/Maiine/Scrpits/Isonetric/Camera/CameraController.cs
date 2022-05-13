using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTrans;
    Vector3 midCamera;
    Vector3 pointToView;

    public float speed;
    public float rotationAngle;
    public float cameraDist;
    public float maxDistX;
    public float maxDistZ;

    delegate void CameraMove();
    CameraMove actualMovement;
    void Start()
    {
        EventManager.Subscribe("SeeObject", SeeObject);

        transform.parent = null;
        GameObject myMain = Camera.main.gameObject;
        myMain.transform.parent = transform;
        transform.rotation = Quaternion.Euler(new Vector3(rotationAngle, 0, 0));
        myMain.transform.localPosition = new Vector3(0, 0, cameraDist * -1);

        actualMovement = FollowPlayer;
    }

    private void Update()
    {
        actualMovement();

        if (Input.GetMouseButtonDown(1))
            SetAim(true);
        else if (Input.GetMouseButtonUp(1))
            SetAim(false);
    }

    public void SetAim(bool isAiming)
    {
        if (isAiming)
            actualMovement = Aim;
        else
            actualMovement = FollowPlayer;
    }

    void FollowPlayer()
    {
        transform.position += (playerTrans.position - transform.position) * speed * Time.deltaTime;
    }

    void Aim()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            Vector3 rayPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 dist = (rayPoint - playerTrans.position) * 0.5f;
            midCamera = rayPoint - dist;

            if (midCamera.x > maxDistX)
                midCamera.x = maxDistX;
            else if(midCamera.x < -maxDistX)
                midCamera.x = -maxDistX;

            if (midCamera.z > maxDistZ)
                midCamera.z = maxDistZ;
            else if (midCamera.z < -maxDistZ)
                midCamera.z = -maxDistZ;

            transform.position += (midCamera - transform.position) * speed * Time.deltaTime;
        }
    }

    void SeePoint()
    {
        transform.position += (pointToView - transform.position) * speed * Time.deltaTime;
    }

    void SeeObject(params object[] parameter)
    {
        pointToView = (Vector3)parameter[0];
        actualMovement = SeePoint;
        StartCoroutine(SeeObjectoCorroutine((float)parameter[1]));
    }

    IEnumerator SeeObjectoCorroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        actualMovement = FollowPlayer;
    }

}
