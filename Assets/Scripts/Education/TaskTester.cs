using System;
using UnityEngine;


public class TaskTester : MonoBehaviour
{
    public Task task;
    public EducationHandler educationHandler;
    //public int NumberTask = 0;//для TaskEditor



    public void ResetTask()
    {
        educationHandler.EndTask(false);
        if (this.task != null) educationHandler.SetTask(this.task);
    }
    public void ResetTask(Task task)
    {
        educationHandler.EndTask(false);
        if (task != null) educationHandler.SetTask(task);
    }

    public void ResetTask(Task newTask)
    {
        educationHandler.EndTask(false);
        educationHandler.SetTask(newTask);
    }
}
