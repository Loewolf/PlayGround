using UnityEngine;
using UnityEngine.UI;

public class EducationHandler : MonoBehaviour
{
    public Text taskDescription;
    public Text instruction;
    public GameObject descriptionObject;
    public RectTransform instructionObject;

    public Camera joinCamera; // Если камера присоединения включена, то окно с подсказками (instructionObject) следует уменьшить

    private Task task;
    private bool cameraEnabled = false;

    private void Start()
    {
        ChangeInstructionWindowScale();
    }

    private void CheckCameraEnabled()
    {
        if (cameraEnabled != joinCamera.enabled)
        {
            cameraEnabled = joinCamera.enabled;
            ChangeInstructionWindowScale();
        }
    }

    public void SetTextValues(string newTaskDescription, bool instructionsEnabled, string newInstruction)
    {
        taskDescription.text = newTaskDescription;
        if (instructionsEnabled)
        {
            instructionObject.gameObject.SetActive(true);
            instruction.text = newInstruction;
        }
        else
        {
            instructionObject.gameObject.SetActive(false);
        }
    }

    public void SetTask(Task newTask)
    {
        task = newTask;
        task.Take();
        ChangeWindowsActivity(true);
        SetTextValues(task.GetCurrentDescription(), task.instructionsEnabled, task.GetCurrentInstruction());
    }

    public void EndTask(bool result)
    {
        if (task)
        {
            task.TurnIn(result);
            task = null;
        }
        ChangeWindowsActivity(false);
    }

    private void ChangeInstructionWindowScale()
    {
        if (cameraEnabled)
        {
            instructionObject.anchorMax = new Vector3(0.7f, 0f);
        }
        else
        {
            instructionObject.anchorMax = new Vector3(1f, 0f);
        }
    }

    private void ChangeWindowsActivity(bool value)
    {
        descriptionObject.SetActive(value);
        instructionObject.gameObject.SetActive(value);
    }

    private void Update()
    {
        if (task)
        {
            int result = task.CheckStageTask();
            switch (result)
            {
                case 1: // если 1, то промежуточный этап задачи успешно выполнен
                    {
                        SetTextValues(task.GetCurrentDescription(), task.instructionsEnabled, task.GetCurrentInstruction());
                        break;
                    }
                case 2: // если 2, то задача успешно выполнена
                    {
                        // установить число очков за задание, равное value в task

                        EndTask(true);
                        break;
                    }
                case -1: // если -1, то достигнут этап, при котором завершить задачу невозможно, требуется перезапуск задания
                    {
                        // предложить пользователю перезапустить задание или вернуться в меню
                        EndTask(false);
                        break;
                    }
                default: break; // если 0, то выполняется промежуточный этап задачи
            }
            CheckCameraEnabled();
        }
    }
}
