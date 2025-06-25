using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private IMove movement;
    private IInputHandle inputHandle;

    private void Awake()
    {
        TryGetComponent<IMove>(out movement);
        TryGetComponent<IInputHandle>(out inputHandle);
    }

    private void Update()
    {
        movement?.Move(inputHandle.GetInput());

        //weapon.Fire();
    }
}