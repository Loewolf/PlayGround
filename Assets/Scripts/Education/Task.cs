using UnityEngine;
using System;
using System.Collections;

public enum TaskMode
{
    Education,
    Training
}

public interface TaskCurrentValue
{
    int currentValue { get; set; } //хранит текущее кол-во очков
    int currentExtraValue { get; set; } //хранит текущее доп. кол-во очков(для задач доп. тренировки)
}

public class Task : MonoBehaviour, TaskCurrentValue
{
    protected RobotController robot;
    public int currentValue { get; set; }
    public int currentExtraValue { get; set; }
    public TaskMode taskMode { get; set; } // Определяет тип задачи: в режиме обучения или доп. тренировок
    [Header("Наименование задачи")]
    public string taskNamePrefix;
    public string taskNameBody;
    public string taskNameSuffix;
    [HideInInspector] public string taskName; // Название задачи, будет отображаться в меню уровней. Собирается из префикса, основной части и суффикса
    [Header("Свойства задачи")]
    public RobotStateManager specialStateManager;
    public int value; // Количество очков, которое получит пользователь за выполнение задания
    public string[] taskDescriptions; // Набор описаний задачи
    public string[] instructions; // Набор инструкций для решения задачи
    public bool instructionsEnabled; // При false отключает отображение текущей инструкции
    public Vector2Int[] pairs; // Набор из индексов описания и инструкций, который будет выведен на заданном этапе
    private const float completionDelay = 5; // Задержка при завершении задания
    [Header("Таймер")]
    public float timeLimit = 0f; // Количество секунд, выделяемое на задание. Если меньше или равно 0, то таймер не запускается
    protected int stage = 0; // Этап выполнения задания. Стартовый этап имеет индекс 0
    /* если 0, то выполняется промежуточный этап задачи
       если 1, то промежуточный этап задачи успешно выполнен (изменяются описание и инструкции задания)
       если 2, то задача успешно выполнена
       если -1, то достигнут этап, при котором завершить задачу невозможно, требуется перезапуск уровня */
    protected Func<int> stageTask;
    protected string currentDescription;
    protected string currentInstruction;
    protected IEnumerator waitForSeconds;
    private int timerReturn = 0;
    [HideInInspector] public bool isWaitingForCompletion = false; // Этот флаг поднимается при вызове EndTask или TerminateTask, чтобы по истечении времени таймера не происходил сброс задания
    [HideInInspector] public bool isSuccesfullyEnded = false;

    protected void Awake()
    {
        ConnectNameParts();
        waitForSeconds = WaitForSeconds(0, 0);
    }

    private void ConnectNameParts()
    {
        taskName = taskNamePrefix + ' ' + taskNameBody + ' ' + taskNameSuffix;
    }

    public string GetCurrentDescription()
    {
        return currentDescription;
    }

    public string GetCurrentInstruction()
    {
        return currentInstruction;
    }

    public int CheckStageTask()
    {
        return stageTask();
    }

    public float GetTimeWithDelay()
    {
        return (timeLimit > 0 && robot) ? timeLimit + robot.timeForSettingState : 0f;
    }

    protected void SetStage(int nextStage, Func<int> newTask, bool instructionsStatus)
    {
        stage = nextStage;
        stageTask = newTask;
        instructionsEnabled = instructionsStatus && pairs[stage].y < instructions.Length && instructions[pairs[stage].y] != "";
        if (stage < pairs.Length)
        {
            currentDescription = taskDescriptions[pairs[stage].x];
            if (instructionsEnabled)
                currentInstruction = instructions[pairs[stage].y];
            else
                currentInstruction = null;
        }
    }

    protected virtual void EnableTaskGameObjects()
    {
        // Включить и настроить все GameObject'ы, связанные с этим заданием
    }

    protected virtual void DisableTaskGameObjects()
    {
        // Выключить все GameObject'ы, связанные с этим заданием
    }

    protected void RobotSetStartPosition()
    {
        robot.accessoryJoinPoint.UnequipAccessory();
        robot.SetState(specialStateManager?.GetStateByRobotController(robot));
        OnSettingSpecialState();
    }

    protected virtual void OnSettingSpecialState()
    {
        // mainGameObject.cameraController.SetSpecialCamera(CameraController.SpecialCameras.Class_3_Tasks);
    }

    private void DropSpecialState()
    {
        CameraController.instance.SetRegularCamera();
    }

    public void Take(RobotController robotController)
    {
        robot = robotController;
        RobotSetStartPosition();
        EnableTaskGameObjects();
        isWaitingForCompletion = false;
        SetStage(0, Task_0, instructionsEnabled);
    }

    public int TurnIn(bool isCompleted, float valueMultiplier)
    {
        DisableTaskGameObjects();
        DropSpecialState();
        isWaitingForCompletion = false;
        if (isCompleted)
        {
            int newValue = Mathf.FloorToInt(valueMultiplier * value);
            if (taskMode == TaskMode.Education)
            {
                currentValue = currentValue < newValue ? newValue : currentValue;
            }
            else
            {
                currentExtraValue = currentExtraValue < newValue ? newValue : currentExtraValue;
            }
            return value;
        }
        else
            return 0;
    }

    // Рекомендуется использовать данный способ наименования для последующих заданий
    protected virtual int Task_0() // Обязательная функция, отвечающая за выполнение первой части задания
    {
        return 0;
    }

    private void StartEndLoop(int resultValue, bool successValue)
    {
        timerReturn = 0;
        StopCoroutine(waitForSeconds);
        waitForSeconds = WaitForSeconds(completionDelay, resultValue);
        StartCoroutine(waitForSeconds);
        isWaitingForCompletion = true;
        isSuccesfullyEnded = successValue;
        stageTask = EndLoop;
    }

    protected int EndTask() // При успешном завершении задания требуется установить делегат stageTask, равный EndTask
    {
        StartEndLoop(2, true);
        return timerReturn;
    }

    public int TerminateTask() // При неудачном завершении задания требуется установить делегат stageTask, равный TerminateTask
    {
        StartEndLoop(-1, false);
        return timerReturn;
    }

    protected int EndLoop()
    {
        return timerReturn;
    }

    protected IEnumerator WaitForSeconds(float seconds = 0, int returnValue = 2)
    {
        if (seconds>0) yield return new WaitForSeconds(seconds);
        timerReturn = returnValue;
    }

    public void RemoveValue()
    {
        currentValue = 0;
        currentExtraValue = 0;
    }
}
