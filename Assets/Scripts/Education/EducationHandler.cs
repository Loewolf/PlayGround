using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EducationHandler : MonoBehaviour
{
    public static EducationHandler instance;

    [Space(10)]
    public Text taskDescription;
    public Text instruction;
    public MaskableGraphic descriptionObject;
    public MaskableGraphic instructionObject;

    [Header("UI Эффекты")]
    public float effectDuration = 1f;

    [Header("Таймер")]
    public TimerContent timerContent;
    public string timeoutText;

    private Task task = null;
    private bool taskValuesAreSet = false;
    private Task previousTask = null;
    private IEnumerator maskFlicker;
    private float alpha = 1f;
    private float step;

    private void Awake()
    {
        if (instance)
        {
            Debug.Log("Instance of EducationHandler already exists");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private float timeLeft = 0f;
    private bool timerIsUsed = false;

    private IEnumerator MaskFlicker(string newTaskDescription = null, bool instructionsEnabled = false, string newInstruction = null)
    {
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * step;
            SetMaskAlpha(alpha * alpha);
            yield return null;
        }
        alpha = 0;

        SetTextValues(newTaskDescription, instructionsEnabled, newInstruction);
        if (task.isWaitingForCompletion)
        {
            if (task.isSuccesfullyEnded)
                timerContent.SetEndColor();
            else
                timerContent.SetTerminateColor();
            timerIsUsed = false;
        }

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * step;
            SetMaskAlpha(alpha * alpha);
            yield return null;
        }
        alpha = 1f;
        SetMaskAlpha(alpha);
    }

    private void SetMaskAlpha(float alpha)
    {
        Color result = Color.white * alpha;
        descriptionObject.color = result;
        instructionObject.color = result;
    }

    private void ResetEffect()
    {
        StopCoroutine(maskFlicker);
        alpha = 1f;
        SetMaskAlpha(alpha);
    }

    private void Start()
    {
        ChangeWindowsActivity(false);
        maskFlicker = MaskFlicker(null, false, null);
        SetMaskAlpha(1f);
        step = 2f / effectDuration;
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

    private void SetTimer(float value)
    {
        if (value <= 0)
        {
            timerIsUsed = false;
        }
        else
        {
            timerIsUsed = true;
            timeLeft = value;
            timerContent.UpdateTime(timeLeft);
            timerContent.SetDefaultColor();
        }
        timerContent.gameObject.SetActive(timerIsUsed);
    }

    public void SetTask(Task newTask)
    {
        task = newTask;
        if (DarkScreen.instance)
        {
            DarkScreen.instance.ExecuteInDarkScreen(SetValuesFromTask);
        }
        else
        {
            SetValuesFromTask();
        }
    }

    private void SetValuesFromTask()
    {
        if (RobotSelector.instance)
        {
            task.Take(RobotSelector.instance.SelectedRobotController);
            ChangeWindowsActivity(true);
            SetTextValues(task.GetCurrentDescription(), task.instructionsEnabled, task.GetCurrentInstruction());
            SetTimer(task.GetTimeWithDelay());
            ResetEffect();
            taskValuesAreSet = true;
        }
    }

    public void EndTask(bool result)
    {
        if (taskValuesAreSet)
        {
            previousTask = task;
            task.TurnIn(result);
            task = null;
            taskValuesAreSet = false;
        }
        ChangeWindowsActivity(false);
    }

    private void ChangeWindowsActivity(bool value)
    {
        descriptionObject.gameObject.SetActive(value);
        instructionObject.gameObject.SetActive(value);
    }

    private void Update()
    {
        if (taskValuesAreSet)
        {
            UpdateTime();
            int result = task.CheckStageTask();
            switch (result)
            {
                case 1: // если 1, то промежуточный этап задачи успешно выполнен
                    {
                        StopCoroutine(maskFlicker);
                        maskFlicker = MaskFlicker(task.GetCurrentDescription(), task.instructionsEnabled, task.GetCurrentInstruction());
                        StartCoroutine(maskFlicker);
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
        }
    }

    private void UpdateTime()
    {
        if (timerIsUsed)
        {
            timeLeft -= Time.deltaTime;
            timerContent.UpdateTime(timeLeft);
            if (timeLeft <= 0 && !task.isWaitingForCompletion)
            {
                task.TerminateTask();
                StopCoroutine(maskFlicker);
                maskFlicker = MaskFlicker(timeoutText, false, null);
                StartCoroutine(maskFlicker);
                timerIsUsed = false;
            }
        }
    }

    public void ResetTask()
    {
        if (task)
        {
            EndTask(false);
            SetTask(previousTask);
        }
        else if (previousTask)
        {
            SetTask(previousTask);
        }
    }

    public void DropAndSetTask(Task task)
    {
        DropTask();
        if (task) SetTask(task);
    }

    public void DropTask()
    {
        EndTask(false);
    }
}
