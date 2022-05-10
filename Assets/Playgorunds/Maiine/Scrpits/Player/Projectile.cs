using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform _parent;
    Transform _objective;

    public float speed;
    public float minDistance;

    public bool isOnParent = true;
    public bool isOnObjective = false;

    delegate void ProjectileMovement();
    ProjectileMovement actualMovement = delegate { };

    void Update()
    {
        actualMovement();
    }

    public void SetObjective(Transform objective)
    {
        _objective = objective;
    }

    public void SetBack()
    {
        if (!isOnParent && isOnObjective)
        {
            Debug.Log("or here?");
            isOnObjective = false;
            actualMovement = BackToParent;
        }
    }

    public void SetGo()
    {
        if (isOnParent && !isOnObjective)
        {
            Debug.Log("here?");
            isOnParent = false;
            actualMovement = GoToObjective;
        }
    }

    void BackToParent()
    {
        if (Vector3.Distance(transform.position, _parent.position) > minDistance)
        {
            transform.LookAt(_parent);
            transform.position += (_parent.position - transform.position) * speed * Time.deltaTime;
            //actualMovement = delegate { };
        }
        else if (isOnParent == false)
            isOnParent = true;
    }

    void GoToObjective()
    {
        Debug.Log("a");
        transform.LookAt(_objective);
        transform.position += (_objective.position - transform.position) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _objective.gameObject)
        {
            actualMovement = delegate { };
            isOnObjective = true;
        }
    }
}
