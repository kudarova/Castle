using UnityEngine;

public class ChunkOptimisationTest : MonoBehaviour
{
    private Transform camRig;
    private bool[] directions = new bool[2];
    private float sideAngle;

    private void Start()
    {
        Camera cam = Camera.main;
        camRig = cam.transform.parent;

        sideAngle = Screen.width / Screen.height * cam.fieldOfView / 2;
    }

    private void Update()
    {

    }

    private void SwapChunks()
    {
        UpdateDirections(camRig.forward);
    }

    private void UpdateDirections(Vector3 rotation)
    {
        directions[0] = rotation.x > 0 ? true : false;
        directions[1] = rotation.z > 0 ? true : false;
    }
}