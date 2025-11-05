using UnityEngine;

public class DebugMatrices : MonoBehaviour
{
  public Camera debugCamera;
  public Transform cubeRed, cubeGreen, cubeBlue;
  public Vector3 sampleLocalVertex = new Vector3(0.5f, 0.5f, 0.5f);

  void Start()
  {
    if (debugCamera == null) debugCamera = Camera.main;
    PrintAll();
  }

  void PrintAll()
  {
    // Camera matrices
    Matrix4x4 view = debugCamera.worldToCameraMatrix;
    Matrix4x4 proj = GL.GetGPUProjectionMatrix(debugCamera.projectionMatrix, false);
    Debug.Log("View Matrix:\n" + view.ToString());
    Debug.Log("Projection Matrix:\n" + proj.ToString());

    // Model matrix and transformed points
    ShowFor(cubeRed, "Red");
    ShowFor(cubeGreen, "Green");
    ShowFor(cubeBlue, "Blue");
  }

  void ShowFor(Transform t, string name)
  {
    Matrix4x4 model = t.localToWorldMatrix;
    Vector4 localPoint = new Vector4(sampleLocalVertex.x, sampleLocalVertex.y, sampleLocalVertex.z, 1f);

    Vector4 worldPoint = model * localPoint;
    Vector4 cameraPoint = debugCamera.worldToCameraMatrix * worldPoint;
    Vector4 clipPoint = debugCamera.projectionMatrix * cameraPoint;
    Vector3 ndcPoint = new Vector3(clipPoint.x / clipPoint.w, clipPoint.y / clipPoint.w, clipPoint.z / clipPoint.w);

    Debug.Log($"--- {name} ---");
    Debug.Log("Model Matrix:\n" + model.ToString());
    Debug.Log($"{name} local -> world: " + worldPoint);
    Debug.Log($"{name} world -> camera: " + cameraPoint);
    Debug.Log($"{name} clip: " + clipPoint);
    Debug.Log($"{name} NDC: " + ndcPoint);

    Vector2 viewportPoint = new Vector2((ndcPoint.x + 1f) * 0.5f, (ndcPoint.y + 1f) * 0.5f);
    Debug.Log($"{name} viewport: " + viewportPoint);
  }
}
