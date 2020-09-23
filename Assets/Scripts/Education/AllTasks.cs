using UnityEngine;

public class AllTasks : MonoBehaviour
{
    //уровни нельзя меняться местами, если уже начался сеанс пользователя.
    //если порядок был изменен, то необходимо сбросить все очки, полученные пользователем (начать заново)
    private Task[] Tasks;

    public void SetTasks()
    {
        int count = transform.childCount;
        Tasks = new Task[count];
        for (int i = 0; i < count; ++i)
        {
            Tasks[i] = transform.GetChild(i).GetComponent<Task>();
        }
    }

    public Task this[int key]
    {
        get => Tasks[key];
    }

    public int Length
    {
        get => Tasks.Length;
    }
}
