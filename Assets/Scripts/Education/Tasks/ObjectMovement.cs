using UnityEngine;

public class ObjectMovement : Task
{
    public bool showInstructions = true; // При false отключает отображение всех инструкций для этого задания
    [Header("Объекты, связанные с задачей")]
    public Transform grabTransform;
    public Grab grab;
    public Transform grabDefaultPoint;
    public PointOfInterest pointOfInterest;
    public AttachableObject[] targetObjects;
    public Transform[] targetObjectsDefaultPositions;
    public GameObject[] otherObjects;
    public Marker marker;

    private int currentObject = 0;
    private int length;

    protected override void EnableTaskGameObjects()
    {
        length = targetObjects.Length;
        for (int i=0; i<length; ++i)
        {
            Transform parent = targetObjects[i].transform.parent;
            parent.gameObject.SetActive(true);
            parent.position = targetObjectsDefaultPositions[i].position;
            parent.rotation = targetObjectsDefaultPositions[i].rotation;
        }
        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(true);
        }
        grabTransform.gameObject.SetActive(true);
        grabTransform.transform.position = grabDefaultPoint.position;
        grabTransform.transform.rotation = grabDefaultPoint.rotation;
        if (!showInstructions) instructionsEnabled = false; 
    }

    protected override void DisableTaskGameObjects()
    {
        mainGameObject.UnequipAccessory();
        for (int i = 0; i < length; ++i)
        {
            targetObjects[i].transform.parent.gameObject.SetActive(false);
        }
        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(false);
        }
        grabTransform.gameObject.SetActive(false);
        pointOfInterest.gameObject.SetActive(false);
        marker.gameObject.SetActive(false);
    }

    protected override int Task_0() // Присоединить оборудование
    {
        if (mainGameObject.GetEquipped())
        {
            pointOfInterest.gameObject.SetActive(true);
            currentObject = 0;
            SetStage(currentObject + 1, Task_ObjectSelector, showInstructions);
        }
        return 0;
    }

    private int Task_ObjectSelector() // Выбрать следующий объект
    {
        if (currentObject < length)
        {
            marker.gameObject.SetActive(true);
            marker.SetTarget(targetObjects[currentObject].GetComponent<Collider>());
            pointOfInterest.targetObject = targetObjects[currentObject].gameObject;
            pointOfInterest.ResetReached();
            SetStage(currentObject + 1, Task_1, showInstructions);
        }
        else
        {
            SetStage(length + 1, EndTask, false);
        }
        return 1;
    }

    private int Task_1() // Взять объект
    {
        if (grab.GetAttachedObject() == targetObjects[currentObject])
        {
            marker.gameObject.SetActive(false);
            SetStage(currentObject + 1, Task_2, showInstructions);
        }
        return 0;
    }

    private int Task_2() // Сбросить объект в указанную область
    {
        if (pointOfInterest.IsReached()&&!grab.GetAttachedObject())
        {
            currentObject++;
            SetStage(currentObject + 1, Task_ObjectSelector, showInstructions);
        }
        else
        {
            if (grab.GetAttachedObject() != targetObjects[currentObject])
            {
                marker.gameObject.SetActive(true);
                SetStage(currentObject + 1, Task_1, showInstructions);
            }
        }
        return 0;
    }
}
