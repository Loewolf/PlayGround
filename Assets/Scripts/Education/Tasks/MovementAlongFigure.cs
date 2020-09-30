using UnityEngine;

public class MovementAlongFigure : Task
{
    [Header("Объекты, связанные с задачей")]
    public RoinStatesKit specialState;
    public ReachablePointNoColliderCheck reachablePoint;
    public LineRenderer lineRenderer;
    public CircularPath circularPath;
    [Min(3)] public int expectedSegmentsCount;
    private Material material;
    private int positionCount;
    private int currentPosition;
    private float previosProgress;

    private void Start()
    {
        material = lineRenderer.material;
    }

    protected override void SetSpecialState()
    {
        mainGameObject.specialState = specialState;
    }

    protected override void EnableTaskGameObjects()
    {
        reachablePoint.gameObject.SetActive(true);
        lineRenderer.gameObject.SetActive(true);

        positionCount = circularPath.SetPositions(expectedSegmentsCount);
        lineRenderer.positionCount = positionCount;
        lineRenderer.SetPositions(circularPath.positions);
        currentPosition = 0;
        previosProgress = 0;
        UpdatePosition();
    }

    protected override void DisableTaskGameObjects()
    {
        reachablePoint.gameObject.SetActive(false);
        lineRenderer.gameObject.SetActive(false);
    }

    private bool UpdatePosition()
    {
        currentPosition++;
        if (currentPosition > positionCount)
        {
            material.SetFloat("_Value1", 1f);
            material.SetFloat("_Value2", 1f);
            return true;
        }
        else
        {
            Vector3 newPosition = Vector3.zero;
            float currentProgress = 0;
            circularPath.GetPropertiesByIndex(currentPosition, ref newPosition, ref currentProgress);
            reachablePoint.transform.position = newPosition;
            reachablePoint.ResetReached();
            material.SetFloat("_Value1", previosProgress);
            material.SetFloat("_Value2", currentProgress);
            previosProgress = currentProgress;
            return false;
        }
    }

    protected override int Task_0() // Поместить все объекты в ящик
    {
        if (mainGameObject.GetAllowMove())
        {
            SetStage(2, TerminateTask, false); // Задание прервано. Робот должен оставаться неподвижным
            return 1;
        }
        else
        {
            if (reachablePoint.IsReached())
            {
                if (UpdatePosition())
                {
                    SetStage(1, EndTask, false); // Задание пройдено
                    return 1;
                }
                else return 0;
            }
            else return 0;
        }
    }
}
