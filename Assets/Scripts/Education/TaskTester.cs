using System;
using UnityEngine;

public class TaskTester : MonoBehaviour
{
    public Task task;
    public EducationHandler educationHandler;

    public void ResetTask()
    {
        educationHandler.EndTask(false);
        if (task != null) educationHandler.SetTask(task);
    }

    public void ResetTask(Task task)
    {
        educationHandler.EndTask(false);
        if (task != null) educationHandler.SetTask(task);
    }
}
