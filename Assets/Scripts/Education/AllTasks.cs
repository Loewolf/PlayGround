using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTasks : MonoBehaviour
{
    //уровни нельзя меняться местами, если уже начался сеанс пользователся.
    //если поменяли, то необхдимо сбросить все его очки(начать заново)
    public Task[] Tasks;
}
