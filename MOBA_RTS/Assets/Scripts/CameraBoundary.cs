using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public enum Boundary
    {
        Up,
        Down,
        Right,
        Left,
    }
    GameObject controller;
    [SerializeField]
    Boundary boundary;

    private void Start()
    {
        controller = GameObject.Find("Controller");
    }

    public void controlCamera(bool isTurnOn)
    {
        controller.GetComponent<Controller>().MoveCamera(boundary, isTurnOn);
    }
}
