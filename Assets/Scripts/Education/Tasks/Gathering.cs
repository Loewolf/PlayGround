
using UnityEngine;

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
    [Range(1, 4)] public int oneTypeCount; // Сколько раз каждый из случайных объектов, дочерних к randomObjectsContainer, будет сгенерирован

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

        destroyingArea.SetRequiredObjectsAmount(randomObjectsContainer.childCount * oneTypeCount);
    }

    protected override void DisableTaskGameObjects()
    {
        mainGameObject.UnequipAccessory();
        grabTransform.gameObject.SetActive(false);
        cameraController.SetRegularCamera();

        crateTransform.gameObject.SetActive(false);
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
