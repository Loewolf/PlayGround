using System.Collections.Generic;
using UnityEngine;

public class AllTasks : MonoBehaviour
{
    //уровни нельзя менять местами, если уже начался сеанс пользователя.
    //если порядок был изменен, то необходимо сбросить все очки, полученные пользователем (начать заново)
    private List<Task> Tasks;

    public void SetTasks()
    {
        int count = transform.childCount;
        Tasks = new List<Task>();
        for (int i = 0; i < count; ++i)
        {
            Transform child = transform.GetChild(i);
            Task task = child.GetComponent<Task>();
            if (!task) // Встретили каталог
            {
                int innerCount = child.childCount;
                for (int j = 0; j < innerCount; ++j)
                {
                    Tasks.Add(child.GetChild(j).GetComponent<Task>());
                }
            }
            else Tasks.Add(task);
        }
    }

    public Task this[int key]
    {
        get => Tasks[key];
    }

    public int Length
    {
        get => Tasks.Count;
    }
}
