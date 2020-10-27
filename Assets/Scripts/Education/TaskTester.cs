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
        if (task == null)
            return;
        this.task = task;
        educationHandler.SetTask(this.task);
    }
}
