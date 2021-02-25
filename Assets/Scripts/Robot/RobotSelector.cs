using System.Collections.Generic;
using UnityEngine;

public class RobotSelector : MonoBehaviour
{
    public static RobotSelector instance;

    public List<RobotController> availableRobotControllers;
    public RobotController SelectedRobotController { get; private set; }

    private void Awake()
    {
        if (instance)
        {
            Debug.Log("Instance of RobotSelector already exists");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (availableRobotControllers.Count == 1) SelectRobot(0);
    }

    private void SelectRobot(int index)
    {
        if (SelectedRobotController) SelectedRobotController.enabled = false;
        SelectedRobotController = availableRobotControllers[index];
        SelectedRobotController.enabled = true;
        CameraController.instance?.SetCamerasContainers(SelectedRobotController);
    }
}
