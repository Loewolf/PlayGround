using UnityEngine;

public class TaskTester : MonoBehaviour
{
    public Task task;
    public EducationHandler educationHandler;

    void Start()
    {
        educationHandler.SetTask(task);
    }
}
