using System.Collections.Generic;
using UnityEngine;

public class ArmConfigurationAtThePoint : Task
{
    [System.Serializable]
    public class RobotStaticArmControllerState
    {
        public RobotController robot;
        public StaticArmController armController;
        public RobotState armState;
    }

    public List<RobotStaticArmControllerState> armControllerVariations;
    public PointOfInterest pointOfInterest;

    private StaticArmController armController;

    protected override void EnableTaskGameObjects()
    {
        RobotStaticArmControllerState variation = armControllerVariations.Find(element => element.robot == robot);
        armController = variation.armController;
        armController.SetState(variation.armState);
        pointOfInterest.transform.position = variation.armState.statePoint.position;
        pointOfInterest.gameObject.SetActive(true);
    }

    protected override void DisableTaskGameObjects()
    {
        pointOfInterest.gameObject.SetActive(false);
        armController.gameObject.SetActive(false);
    }

    protected override int Task_0()
    {
        if (pointOfInterest.IsReached())
        {
            SetStage(1, Task_1, false);
            return 1;
        }
        else return 0;
    }

    private int Task_1()
    {
        if (robot.RotationsAllowed)
        {
            armController.transform.position = robot.transform.position;
            armController.transform.rotation = robot.transform.rotation;
            armController.gameObject.SetActive(true);
            SetStage(2, Task_2, false);
            return 1;
        }
        else
        {
            if (pointOfInterest.IsReached())
            {
                return 0;
            }
            else
            {
                SetStage(0, Task_0, false);
                return 1;
            }
        }
    }

    private int Task_2()
    {
        if (ComparePositions())
        {
            armController.gameObject.SetActive(false);
            SetStage(3, EndTask, false);
            return 1;
        }
        else
        {
            if (robot.RotationsAllowed)
            {
                return 0;
            }
            else
            {
                armController.gameObject.SetActive(false);
                SetStage(1, Task_1, false);
                return 1;
            }
        }
    }

    private bool ComparePositions()
    {
        for (int i = 0; i < robot.articulationBodyRotations.Count; ++i)
        {
            if (Vector3.SqrMagnitude(robot.articulationBodyRotations[i].transform.position - armController.rotations[i].transform.position) > 0.0025f)
            {
                return false;
            }
        }
        return true;
    }
}