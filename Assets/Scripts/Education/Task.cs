using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;

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
public class Task: MonoBehaviour, TaskCurrentValue
{
    public Example mainGameObject;
    public int currentValue { get; set; }
    public int currentExtraValue { get; set; }
    public TaskMode taskMode { get; set; }//этот таск в режиме обучения или доп. тренировок?

    [Header("Свойства задачи")]
    public string taskName; //название задачи, будет отображаться в меню уровней
    public Transform startPoint; // Точка, в которую будет отправлен робот при старте выполнения задания
    public int value; // Количество очков, которое получит пользователь за выполнение задания
    public string[] taskDescriptions; // Набор описаний задачи
    public string[] instructions; // Набор инструкций для решения задачи
    public bool instructionsEnabled; // При false отключает отображение текущей инструкции
    public Vector2Int[] pairs; // Набор из индексов описания и инструкций, который будет выведен на заданном этапе
    public float completionDelay = 0; // Задержка при завершении задания
    protected int stage = 0; // Этап выполнения задания. Стартовый этап имеет индекс 0
    /* если 0, то выполняется промежуточный этап задачи
       если 1, то промежуточный этап задачи успешно выполнен (изменяются описание и инструкции задания)
       если 2, то задача успешно выполнена
       если -1, то достигнут этап, при котором завершить задачу невозможно, требуется перезапуск уровня */
    protected Func<int> stageTask;
    protected string currentDescription;
    protected string currentInstruction;
    protected IEnumerator waitForSeconds;
    protected int timerReturn = 0;

    protected void Awake()
    {
        waitForSeconds = WaitForSeconds(0, 0);
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

    protected void SetStage(int nextStage, Func<int> newTask, bool instructionsStatus)
    {
        stage = nextStage;
        stageTask = newTask;
        instructionsEnabled = instructionsStatus;
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

    protected void MainGameObjectSetStartPosition()
    {
        mainGameObject.UnequipAccessory();
        mainGameObject.SetStartValues();
        mainGameObject.transform.position = startPoint.position;
        mainGameObject.transform.rotation = startPoint.rotation;
    }

    public void Take()
    {
        MainGameObjectSetStartPosition();
        EnableTaskGameObjects();
        SetStage(0, Task_0, instructionsEnabled);
    }

    public int TurnIn(bool isCompleted)
    {
        DisableTaskGameObjects();
        if (isCompleted)
        {
            if (taskMode == TaskMode.Education)
            {
                currentValue = currentValue < value ? value : currentValue;
            }
            else
            {
                currentExtraValue = currentExtraValue < value ? value : currentExtraValue;
            }
            return value;
        }
        else
            return 0;
    }

    public void RemoveValue()
    {
        currentValue = 0;
        currentExtraValue = 0;
    }
    
    
    // Рекомендуется использовать данный способ наименования для последующих заданий
    protected virtual int Task_0() // Обязательная функция, отвечающая за выполнение первой части задания
    {
        return 0;
    }

    protected int EndTask() // При завершении задания требуется установить делегат stageTask, равный EndTask
    {
        Debug.Log("Task Completed");
        StopCoroutine(waitForSeconds);
        waitForSeconds = WaitForSeconds(completionDelay, 2);
        StartCoroutine(waitForSeconds);
        stageTask = EndLoop;
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
}
