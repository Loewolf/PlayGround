using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera targetCamera;
    public Transform[] cameraTransforms;
    private int currentCamera;

    void Start()
    {
        currentCamera = 0;
        AttachCameraToTransform(cameraTransforms[currentCamera]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            currentCamera++;
            if (currentCamera >= cameraTransforms.Length) currentCamera = 0;
            AttachCameraToTransform(cameraTransforms[currentCamera]);
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            currentCamera--;
            if (currentCamera < 0) currentCamera = cameraTransforms.Length - 1;
            AttachCameraToTransform(cameraTransforms[currentCamera]);
        }
    }

    public void AttachCameraToTransform(Transform targetTransform)
    {
        targetCamera.transform.SetParent(targetTransform);
        targetCamera.transform.localPosition = Vector3.zero;
        targetCamera.transform.localRotation = Quaternion.identity;
    }
}
