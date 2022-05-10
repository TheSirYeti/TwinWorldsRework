using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Player _player;
    Transform _objective;

    public float speed;
    public float minDistance;

    public bool isOnParent = true;
    public bool isOnObjective = false;

    bool isConected = false;
    bool isTrigger = false;
    IInteractable actualInteractable = null;

    delegate void ProjectileMovement();
    ProjectileMovement actualMovement = delegate { };

    private void Start()
    {
        actualMovement = BackToParent;
    }

    void Update()
    {
        actualMovement();

        if(Input.GetKeyDown(KeyCode.F) && isTrigger && !isConected)
        {
            actualInteractable.StartInteractable(_player);
            isConected = true;
        }
    }

    public void SetObjective(Transform objective)
    {
        _objective = objective;
    }

    public void SetDir()
    {
        if (isOnParent && !isOnObjective)
        {
            isOnParent = false;
            actualMovement = GoToObjective;
        }
        else if (!isOnParent && isOnObjective)
        {
            isOnObjective = false;
            actualMovement = BackToParent;
            actualInteractable.StopInteractable();
            isTrigger = false;
            isConected = false;
        }
    }

    void BackToParent()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > minDistance)
        {
            transform.LookAt(_player.transform);
            transform.position += (_player.transform.position - transform.position) * speed * Time.deltaTime;
        }
        else if (isOnParent == false)
            isOnParent = true;
    }

    void GoToObjective()
    {
        transform.LookAt(_objective);
        transform.position += (_objective.position - transform.position) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _objective.gameObject)
        {
            actualMovement = delegate { };
            isOnObjective = true;

            IInteractable thisInteractable = other.GetComponent<IInteractable>();
            if(thisInteractable != null)
            {
                actualInteractable = thisInteractable;
                isTrigger = true;
            }
        }
    }
}
