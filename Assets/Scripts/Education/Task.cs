using UnityEngine;
using System;

public class Task: MonoBehaviour
{
    [Header("Свойства задачи")]
    public int value; // Количество очков, которое получит пользователь за выполнение задания
    public string[] taskDescriptions; // Набор описаний задачи
    public string[] instructions; // Набор инструкций для решения задачи
    public bool instructionsEnabled;
    public Vector2Int[] pairs; // Набор из индексов описания и инструкций, который будет выведен на заданном этапе

    protected int stage = 0; // Этап выполнения задания. Стартовый этап имеет индекс 0
    /* если 0, то выполняется промежуточный этап задачи
       если 1, то промежуточный этап задачи успешно выполнен (изменяются описание и инструкции задания)
       если 2, то задача успешно выполнена
       если -1, то достигнут этап, при котором завершить задачу невозможно, требуется перезапуск уровня */
    protected Func<int> stageTask;
    protected string currentDescription;
    protected string currentInstruction;

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

    public virtual void Take()
    {
        EnableTaskGameObjects();
        SetStage(0, Task_0, instructionsEnabled);
    }

    public virtual int TurnIn(bool isCompleted)
    {
        DisableTaskGameObjects();
        if (isCompleted)
            return value;
        else
            return 0;
    }

    // Рекомендуется использовать данный способ наименования для последующих заданий
    protected virtual int Task_0() // Обязательная функция, отвечающая за выполнение первой части задания
    {
        return 0;
    }

    protected virtual int FinalStageTask()
    {
        Debug.Log("Task Completed");
        return 2; 
    }
}
