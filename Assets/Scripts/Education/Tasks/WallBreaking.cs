using UnityEngine;

public class WallBreaking : Task
{
    [Header("Объекты, связанные с задачей")]
    public Transform hammerTransform;
    public HydraulicHammer hammer;
    public Transform hammerDefaultPoint;

    public GameObject wallPrefab;
    public Transform wallDefaultPoint;
    public Transform area;
    public PointOfInterest pointOfInterest;
    public Transform grid; // Должен содержать дочерние объекты, имеющие только компонент ReachablePoint
    public GameObject[] otherObjects;

    // Группа полей, определяющих векторы, используемые при вычислении угла гидромолота
    [Header("Вспомогательные точки")]
    public Transform hammerPoint1;
    public Transform hammerPoint2;
    public Transform axisYtop;
    public Transform axisYbottom;
    public Transform axisXright;
    public Transform axisXleft;

    private GameObject instantiatedWall;
    private Vector3 axisX;
    private Vector3 axisY;
    private int reachablePointsAmount;
    private ReachablePoint[] reachablePoints;
    private int[] visitedPoints;
    private const float COS_EPS = 0.15f;

    protected override void EnableTaskGameObjects()
    {
        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(true);
        }
        hammerTransform.gameObject.SetActive(true);
        hammerTransform.transform.position = hammerDefaultPoint.position;
        hammerTransform.transform.rotation = hammerDefaultPoint.rotation;
        instantiatedWall = Instantiate(wallPrefab, wallDefaultPoint.position, wallDefaultPoint.rotation);
        area.gameObject.SetActive(true);

        axisX = axisXright.position - axisXleft.position;
        axisY = axisYtop.position - axisYbottom.position;

        pointOfInterest.gameObject.SetActive(true);
        pointOfInterest.ResetReached();

        reachablePointsAmount = grid.childCount;
        reachablePoints = new ReachablePoint[reachablePointsAmount];
        visitedPoints = new int[reachablePointsAmount];
        for (int i = 0; i < reachablePointsAmount; ++i)
        {
            reachablePoints[i] = grid.GetChild(i).GetComponent<ReachablePoint>();
            reachablePoints[i].ResetReached();
            visitedPoints[i] = 0;
        }
    }

    protected override void DisableTaskGameObjects()
    {
        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(false);
        }
        mainGameObject.UnequipAccessory();
        hammerTransform.gameObject.SetActive(false);
        Destroy(instantiatedWall);
        area.gameObject.SetActive(false);
    }

    protected override int Task_0() // Присоединить оборудование
    {
        if (mainGameObject.GetEquipped())
        {
            SetStage(1, Task_1, false);
            return 1;
        }
        else return 0;
    }

    private bool IsCorrectAngle()
    {
        Vector3 hammerVector = hammerPoint2.position - hammerPoint1.position;
        return (Mathf.Abs(OtherMath.CosOfAngleBetweenTwoVectors(hammerVector, axisX)) < COS_EPS) && (Mathf.Abs(OtherMath.CosOfAngleBetweenTwoVectors(hammerVector, axisY)) < COS_EPS);
    }

    private int Task_1() // Установить молот под углом 90-epsilon градусов
    {
        if (mainGameObject.GetEquipped())
        {
            if (pointOfInterest.IsReached() && IsCorrectAngle())
            {
                pointOfInterest.gameObject.SetActive(false);
                SetStage(2, Task_2, true);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            SetStage(0, Task_0, true);
            return 1;
        }
    }

    private int CountVisitedPoints()
    {
        int amount = 0;
        for (int i = 0; i < reachablePointsAmount; ++i)
        {
            if (reachablePoints[i].IsReached()) visitedPoints[i] = 1;
            amount += visitedPoints[i];
        }
        return amount;
    }

    private int Task_2() // Уничтожить кусок стены
    {
        if (CountVisitedPoints() == reachablePointsAmount)
        {
            SetStage(3, EndTask, false);
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
