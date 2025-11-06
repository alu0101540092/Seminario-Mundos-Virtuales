using UnityEngine;

public class DebugMatricesRealtime : MonoBehaviour
{
  public Camera cam;
  public Transform targetCube;
  private Vector3 prevPosition;
  private Quaternion prevRotation;
  private bool prevOrthographic;

  void Start()
  {
    if (cam == null) cam = Camera.main;
    prevPosition = targetCube.position;
    prevRotation = targetCube.rotation;
    prevOrthographic = cam.orthographic;
    PrintState("Start");
  }

  void Update()
  {
    if (targetCube.position != prevPosition || targetCube.rotation != prevRotation)
    {
      PrintState("Target changed");
      prevPosition = targetCube.position;
      prevRotation = targetCube.rotation;
    }

    if (cam.orthographic != prevOrthographic)
    {
      PrintState("Camera projection changed");
      prevOrthographic = cam.orthographic;
    }
  }

  void PrintState(string reason)
  {
    Debug.Log($"-- {reason} --");
    Debug.Log("Model (local to world) of target:\n" + targetCube.localToWorldMatrix.ToString());
    Debug.Log("View (world to camera):\n" + cam.worldToCameraMatrix.ToString());
    Debug.Log("Projection:\n" + cam.projectionMatrix.ToString());
  }
}
