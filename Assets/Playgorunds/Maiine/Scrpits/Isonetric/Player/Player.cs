using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isActive;

    public GameObject realCharacter;
    public GameObject totemCharacter;

    IPlayerInteractable _playerInteractable = null;

    MovementController myMovementController;
    ButtonsController myButtonController;
    public CameraController cameraController;


    private void Start()
    {
        myMovementController = new MovementController(transform, speed);
        myButtonController = new ButtonsController(this, myMovementController, cameraController);

        EventManager.Subscribe("ChangePlayer", ChangeCharacter);

        if (isActive)
        {
            realCharacter.SetActive(true);
            totemCharacter.SetActive(false);
            myMovementController.ChangeToMove();
            myButtonController.ButtonsOn();
        }
        else
        {
            realCharacter.SetActive(false);
            totemCharacter.SetActive(true);
            myMovementController.ChangeToStay();
            myButtonController.ButtonsOff();
        }
    }

    void Update()
    {
        myButtonController.actualButtons();
        myMovementController.actualMovement();
    }

    public void ChangeCharacter(params object[] parameter)
    {
        Debug.Log("aa?");
        if (isActive)
        {
            myMovementController.ChangeToStay();
            myButtonController.ButtonsOff();
            realCharacter.SetActive(false);
            totemCharacter.SetActive(true);
        }
        else
        {
            StartCoroutine(TimerTurnOn());
        }

        isActive = !isActive;
    }

    public void CheckInteractable()
    {
        if (_playerInteractable == null) return;

        _playerInteractable.DoAction();

        //Mover dentro del objecto
        //EventManager.Trigger("SeeObject", _playerInteractable.GetPos(), _playerInteractable.GetTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        IPlayerInteractable actualPlayerInteractable = other.gameObject.GetComponent<IPlayerInteractable>();
        if (actualPlayerInteractable != null)
        {
            _playerInteractable = actualPlayerInteractable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPlayerInteractable actualPlayerInteractable = other.gameObject.GetComponent<IPlayerInteractable>();
        if (actualPlayerInteractable != null)
            _playerInteractable = null;
    }

    IEnumerator TimerTurnOn()
    {
        yield return new WaitForEndOfFrame();
        myMovementController.ChangeToMove();
        myButtonController.ButtonsOn();
        realCharacter.SetActive(true);
        totemCharacter.SetActive(false);
    }
}
