using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public class AllTasks : MonoBehaviour
{
    //уровни нельзя меняться местами, если уже начался сеанс пользователся.
    //если поменяли, то необхдимо сбросить все его очки(начать заново)
    public Task[] Tasks;
    public Task[] RandomTasks;
    public int CountRandomTasks;
    public Task ResearchTask;

    private void Awake()
    {
        SetRandomTasks();
    }

    //обнуление очков
    public void ResetTask()
    {
        //новые случ. таски
        SetRandomTasks();
        foreach (var task in Tasks)
        {
            task.RemoveValue();
        }
    }
    public void SetTasks(Task[] tasks)
    {
        Tasks = tasks;
        SetRandomTasks();
    }

    private void SetRandomTasks()
    {
        RandomTasks = new Task[CountRandomTasks];
        for (int i = 0; i < CountRandomTasks;)
        {
            Task randomTask = Tasks[Random.Range(0,Tasks.Length)];
            if (!RandomTasks.Contains(randomTask))
            {
                RandomTasks[i] = randomTask;
                i++;
            }
        }
    }
    
    
    
}
