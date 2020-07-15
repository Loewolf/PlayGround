using UnityEngine;
using UnityEngine.UI;

public class EducationHandler : MonoBehaviour
{
    public Text taskDescription;
    public Text instruction;
    public RectTransform instructionObject;

    public Camera joinCamera; // Если камера присоединения включена, то окно с подсказками (instructionObject) следует уменьшить

    private Task task;
    private bool cameraEnabled = false;

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
        SetTextValues(task.GetCurrentDescription(), task.instructionsEnabled, task.GetCurrentInstruction());
    }

    private void ChangeInstructionWindow()
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
                        task.TurnIn(true);
                        task = null;
                        break;
                    }
                case -1: // если -1, то достигнут этап, при котором завершить задачу невозможно, требуется перезапуск задания
                    {
                        // предложить пользователю перезапустить задание или вернуться в меню
                        task.TurnIn(false);
                        task = null;
                        break;
                    }
                default: break; // если 0, то выполняется промежуточный этап задачи
            }
        }
        if (cameraEnabled != joinCamera.enabled)
        {
            cameraEnabled = joinCamera.enabled;
            ChangeInstructionWindow();
        }
    }
}
