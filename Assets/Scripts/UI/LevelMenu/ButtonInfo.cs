using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    private Task _task;
    public Button Button;
    public Text LevelName;
    public Text LevelScore;

    public void UpdateInfo()
    {
        LevelName.text = _task.taskName;
        LevelScore.text = _task.currentValue.ToString();
    }
    
    public void SetTask(Task task, TaskMode mode)
    {
        _task = task;
        _task.taskMode = mode;
        if (mode == TaskMode.Education)
        {
            LevelScore.text = task.currentValue.ToString();
        }
        else
        {
            LevelScore.text = task.currentExtraValue.ToString();
        }
        LevelName.text = task.taskName;
    }

    public Task GetTask()
    {
        return _task; 
    }

    public void SetEvent(UnityAction call)
    {
        Button.onClick.AddListener(call);
    }
}
