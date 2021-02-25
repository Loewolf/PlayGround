using System.Collections.Generic;
using UnityEngine;

public class RobotStateManager : MonoBehaviour
{
    [System.Serializable]
    public struct RobotStatePair
    {
        public RobotController robot;
        public RobotState state;
    }

    public List<RobotStatePair> robotStatePairs;

    public RobotState GetStateByRobotController(RobotController robotController)
    {
        return robotStatePairs.Find(element => element.robot == robotController).state;
    }
}
