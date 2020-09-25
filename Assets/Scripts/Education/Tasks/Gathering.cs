using UnityEngine;
using System.Collections.Generic;

public class Gathering : Task
{
    [Header("Объекты, связанные с задачей")]
    public Transform grabTransform;
    public Grab grab;
    public CameraController cameraController;
    public RoinStatesKit specialState;
    [Space(15)]
    public Transform crateTransform;
    public DestroyingArea destroyingArea;
    [Space(15)]
    public Transform randomObjectsContainer;
    public SpiralPoints spiralPoints;
    [Range(1, 4)] public int oneTypeCount; // Сколько раз каждый из случайных объектов, дочерних к randomObjectsContainer, будет сгенерирован
    private List<Transform> randomObjects;
    private int objectsCount;

    private void Start()
    {
        randomObjects = new List<Transform>();
    }

    protected override void SetSpecialState()
    {
        mainGameObject.specialState = specialState;
    }

    protected override void EnableTaskGameObjects()
    {
        grabTransform.gameObject.SetActive(true);
        mainGameObject.EquipAccessoryWithForce(grab);
        cameraController.SetSpecialCamera(CameraController.SpecialCameras.Class_3_Tasks);

        crateTransform.gameObject.SetActive(true);
        destroyingArea.ResetReached();

        objectsCount = randomObjectsContainer.childCount * oneTypeCount;
        destroyingArea.SetRequiredObjectsAmount(objectsCount);
        destroyingArea.SetGrab(grab);
        spiralPoints.CreateSequence(objectsCount);
        for (int i = 0; i < randomObjectsContainer.childCount; ++i)
        {
            for (int j = 0; j < oneTypeCount; ++j)
            {
                randomObjects.Add(Instantiate(randomObjectsContainer.GetChild(i), 
                                              spiralPoints.GetWorldPositionOfPoint(i * oneTypeCount + j), 
                                              Random.rotation));
            }
        }
    }

    protected override void DisableTaskGameObjects()
    {
        mainGameObject.UnequipAccessory();
        grabTransform.gameObject.SetActive(false);
        cameraController.SetRegularCamera();

        crateTransform.gameObject.SetActive(false);

        foreach (Transform randomObject in randomObjects)
        {
            if (randomObject) Destroy(randomObject.gameObject);
        }
        randomObjects.Clear();
    }

    protected override int Task_0() // Поместить все объекты в ящик
    {
        if (destroyingArea.IsEnoughObjects())
        {
            SetStage(1, EndTask, false); // Задание пройдено
            return 1;
        }
        else if (!mainGameObject.GetEquipped())
        {
            SetStage(2, TerminateTask, false); // Задание прервано. Навесное оборудование было отсоединено
            return 1;
        }
        else if (mainGameObject.GetAllowMove())
        {
            SetStage(3, TerminateTask, false); // Задание прервано. Робот должен оставаться неподвижным
            return 1;
        }
        else return 0;
    }
}
