using UnityEngine;

public class Move : Task
{

    // Start is called before the first frame update
    public bool showInstructions = true; // При false отключает отображение всех инструкций для этого задания
    [Header("Объекты, связанные с задачей")]
    public bool autr;
    public KeyCode[] key;
    public float time = 1f;
    private float timeEnd;

    protected override void EnableTaskGameObjects()
    {
        timeEnd = time;
    }

    protected override void DisableTaskGameObjects()
    {
    }

    protected override int Task_0()
    {
        bool t = false;
        for (int i = 0; i < key.Length; i++)
            if (Input.GetKey(key[i]))
                t = true;
        if (t)
        {
            if (timeEnd > 0)
                timeEnd -= Time.deltaTime;
            else
            {
                SetStage(2, EndTask, showInstructions);
                return 1;
            }
        }
        return 0;
    }
}
