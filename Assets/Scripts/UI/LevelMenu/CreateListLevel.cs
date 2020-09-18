using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateListLevel : MonoBehaviour
{
    public AllTasks AllTasks;
    public TaskTester TaskTester;
    private List<ButtonInfo> _buttons = new List<ButtonInfo>();
    
    [Header("Для генерации кнопок")]
    public GameObject Button;
    public GameObject TransformParent;

    private void Awake()
    {
        //при первом запуске игры генерируем кнопки
        
        int levelCount = AllTasks.Length;
        for (int i = 0; i < levelCount; i++)
        {
            var button = Instantiate(Button, TransformParent.transform).GetComponent<ButtonInfo>();
            _buttons.Add(button);
            var task = AllTasks[i];
            button.Button.onClick.AddListener(() => TaskTester.ResetTask(task));
            button.LevelName.text = task.taskName;
            button.LevelScore.text = task.currentValue.ToString();
        }
    }

    private void OnEnable()
    {
        //при каждом открытии обновляем очки уровней
        int levelCount = AllTasks.Length;
        for (int i = 0; i < levelCount; i++)
        {
            var task = AllTasks[i];
            _buttons[i].LevelScore.text = task.currentValue.ToString();

        }
    }
}
