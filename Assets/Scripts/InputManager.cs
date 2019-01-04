using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public static InputManager Instance { get; private set; } = new InputManager();
    private InputManager() { }

    public bool HasMoveUp() { return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0; }
    public bool HasMoveDown() { return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0; }
    public bool HasMoveRight() { return Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0; }
    public bool HasMoveLeft() { return Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0; }
    public bool HasOk() { return Input.GetButtonDown("Submit"); }
    public bool HasCancle() { return Input.GetButtonDown("Cancel"); }
}
