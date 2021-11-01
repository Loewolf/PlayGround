using UnityEngine;

public class RailsTask : Task
{
    [SerializeField] private RailsModel _startingRails;

    protected override void EnableTaskGameObjects() // ���� ����� ������� ����������� ��� ������ ����������� base
    {
        _SetOnRails();
    }

    private void _SetOnRails()
    {
        if (robot.movement.GetType() == typeof(RailsMovement))
        {
            RailsMovement movement = (RailsMovement)robot.movement;
            movement.SetOnRails(_startingRails);
        }
        else
        {
            Debug.Log("���� ����� �� ������������ �� �������. �������� ���������� ������� ����� ���� ����������");
        }
    }
}