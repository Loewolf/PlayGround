using UnityEngine;

public class AllTasks : MonoBehaviour
{
    //уровни нельзя меняться местами, если уже начался сеанс пользователся.
    //если поменяли, то необхдимо сбросить все его очки(начать заново)
    public Task[] Tasks;

    public Task this[int key]
    {
        get => Tasks[key];
    }

    public int Length
    {
        get => Tasks.Length;
    }
}
