using UnityEngine;

public class TaskTester : MonoBehaviour
{
    public Task task;
    public EducationHandler educationHandler;

    public void ResetTask()
    {
        educationHandler.EndTask(false);
        if (task) educationHandler.SetTask(task);
    }
}
