using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : IInputHandler
{

    private IMovementAndMeleeCombatLogic inputLogic;
    private IMovementAndMeleeCombatInput model;
    private IMove movement;

    private int db = 0;

    public MoveHandler(IMovementAndMeleeCombatLogic inputLogic, IMovementAndMeleeCombatInput model, IMove movement)
    {
        this.inputLogic = inputLogic;
        this.model = model;
        this.movement = movement;
        this.inputLogic.InputChanged += InputLogic_InputChanged;
    }

    public void Update()
    {
        inputLogic.ReadInput();
    }

    public void LateUpdate()
    {

    }

    private void InputLogic_InputChanged(IMovementAndMeleeCombatInput obj)
    {
        db++;
        Debug.Log("Received event: " + db);
        this.movement.Move(obj.MovementPosX, obj.MovementPosY, 2, Time.deltaTime);
    }
}
