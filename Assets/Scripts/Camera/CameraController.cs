using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    public Camera targetCamera;
    public Transform regularCamerasContainer;
    private Transform[] regularCamerasTransforms;
    private int currentCamera;

    public Transform specialCamerasContainer;
    private Transform[] specialCamerasTransforms;
    private bool isUsingSpecialCamera = false;

    public enum SpecialCameras { Class_3_Tasks };

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance of CameraController " + " already exists");
            Destroy(this);
        }
    }

    void Start()
    {
        int count = regularCamerasContainer.childCount;
        regularCamerasTransforms = new Transform[count];
        for (int i = 0; i < count; ++i)
            regularCamerasTransforms[i] = regularCamerasContainer.GetChild(i);
        currentCamera = 0;
        AttachCameraToTransform(regularCamerasTransforms[currentCamera]);

        count = specialCamerasContainer.childCount;
        specialCamerasTransforms = new Transform[count];
        for (int i = 0; i < count; ++i)
            specialCamerasTransforms[i] = specialCamerasContainer.GetChild(i);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket) && !isUsingSpecialCamera)
        {
            currentCamera++;
            if (currentCamera >= regularCamerasTransforms.Length) currentCamera = 0;
            AttachCameraToTransform(regularCamerasTransforms[currentCamera]);
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket) && !isUsingSpecialCamera)
        {
            currentCamera--;
            if (currentCamera < 0) currentCamera = regularCamerasTransforms.Length - 1;
            AttachCameraToTransform(regularCamerasTransforms[currentCamera]);
        }
    }

    public void SetSpecialCamera(SpecialCameras specialCamera)
    {
        isUsingSpecialCamera = true;
        AttachCameraToTransform(specialCamerasTransforms[(int)specialCamera]);
    }

    public void SetRegularCamera()
    {
        isUsingSpecialCamera = false;
        AttachCameraToTransform(regularCamerasTransforms[currentCamera]);
    }

    public void AttachCameraToTransform(Transform targetTransform)
    {
        targetCamera.transform.SetParent(targetTransform);
        targetCamera.transform.localPosition = Vector3.zero;
        targetCamera.transform.localRotation = Quaternion.identity;
    }
}
