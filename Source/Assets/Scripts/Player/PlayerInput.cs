using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool shoot;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool dodge;

    bool readyToClear; //Esta variable indicará si el input debe limpiarse o no en el siguiente Update().

    [Header("Offset mínimo del stick")]
    [Tooltip("Sirve para que el stick solo funcione cuando llegue a una posicion mínima.")]
    public float minStickOffset = 0.2f;

    // Update is called once per frame
    void Update()
    {
        ClearInput();

        ProcessInput();

        //Clampeamos los ejes x e y
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    private void FixedUpdate()
    {
        readyToClear = true; //Este flag permite que el input se limpie durante
                             //el Update y asegura que no se va a perder ningún input.
    }

    void ClearInput()
    {
        if (!readyToClear)
        {
            return;
        }

        //Limpiamos las variables
        horizontal = 0f;
        shoot = false;
        jump = false;
        dodge = false;

        readyToClear = false;
    }

    void ProcessInput()
    {
        //Acumulamos horizontal y vertical
        if (Input.GetAxisRaw("Horizontal") > minStickOffset || Input.GetAxisRaw("Horizontal") < -minStickOffset)
        {
            horizontal += Input.GetAxisRaw("Horizontal");
        }

        //Acumulamos inputs de los botones
        shoot = shoot || Input.GetButtonDown("Shoot");
        dodge = dodge || Input.GetButtonDown("Dodge");
        jump = jump || Input.GetButtonDown("Jump");
    }
}
