using UnityEngine;

// IVE BEEN LOOKING FOR TOO LONG TO CHANGE THE CAMERA LIMIT
// I DONT LIKE, CAMERA IS MASSIVE STRUCTURE SUCH AS WORLD FOR EXAMPLE, NEED TO REWRITE
// make full free camera movement

public class CameraController : MonoBehaviour 
{
    private Camera cam;
    private Transform camRig;
    private World world;

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 localPosition;

    private Vector3 startPosition;
    private Vector3 startRotation;

    public int mouseWheelSpeed = 100; // settings
    public float smoothMovement = 0f; // settings
    public float rotationSpeed = .2f; // settings mb sensetive
    private float xRotation = 45; // automation thing

    private float terrainHeight; // just test, dont know how to make it normal
    private float heightPos;

    private void Start()
    {
        cam = Camera.main;
        camRig = cam.transform.parent;
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        camRig.position = new Vector3(WorldData.VoxelWidth / 2, 40, WorldData.VoxelWidth / 2); // WHAT ARE THIS NUMBERS
        terrainHeight = ChunkData.VoxelHeight * Noise.Get2DPerlin(new Vector2(camRig.position.x, camRig.position.z), world.seed, world.offset);

        localPosition = transform.localPosition;
        position = camRig.position;
        rotation = camRig.rotation;
        heightPos = position.y;
    }
    private void Update()
    {
        CameraMovement();
        CameraRotation();
        CameraZoom();

        GetHeightDifference();
        position.y = heightPos;

        camRig.position = Vector3.Lerp(camRig.position, position, smoothMovement * Time.deltaTime);
        camRig.rotation = Quaternion.Lerp(camRig.rotation, rotation, smoothMovement * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(xRotation, 0, 0)), smoothMovement * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, localPosition, smoothMovement * Time.deltaTime);
    }

    private void CameraMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startPosition = NormalizeCameraInput();
        }
        else if (Input.GetMouseButton(1))
        {
            position = camRig.position;
            Vector3 targetPosition = NormalizeCameraInput() - startPosition;

            int negativeLimit = -30;
            int positiveLimit = WorldData.VoxelWidth + 30;
            position.x = Mathf.Clamp(position.x - targetPosition.x, negativeLimit, positiveLimit);
            position.z = Mathf.Clamp(position.z - targetPosition.z, negativeLimit, positiveLimit);
        }
        if (Input.GetKey(KeyCode.W))
        {

        }
    }

    private void CameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            startRotation = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            float yRotation = (Input.mousePosition - startRotation).x * rotationSpeed;
            rotation *= Quaternion.Euler(new Vector3(0, yRotation, 0));
            startRotation = Input.mousePosition;
        }
    }

    private void CameraZoom()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            // change rotation while zooming
            float height = -mouseWheel * (transform.position.y / 9); // WHAT ARE THIS NUMBERS
            xRotation = Mathf.Clamp(xRotation + height, 35, 45);
            localPosition.z += height;

            // zoom
            mouseWheel *= mouseWheelSpeed;
            localPosition += new Vector3(0, -mouseWheel, mouseWheel);
            localPosition.y = Mathf.Clamp(localPosition.y, 10, 100);
            localPosition.z = Mathf.Clamp(localPosition.z, -100, -20);
        }
    }

    private void GetHeightDifference() // NOT LIKE THIS
    {
        float heightDiference = ChunkData.VoxelHeight * Noise.Get2DPerlin(new Vector2((int)camRig.position.x, (int)camRig.position.z), world.seed, world.offset) - terrainHeight;
        terrainHeight += heightDiference;
        heightPos += heightDiference;
    }

    private Vector3 NormalizeCameraInput()
    {
        Vector3 screenPosition = Input.mousePosition * camRig.position.y;
        screenPosition.z = cam.nearClipPlane + 1;

        return cam.ScreenToWorldPoint(screenPosition);
    }

    public void ToSpawn()
    {

    }
    public void ToCenter()
    {

    }
    public void SetPosition(Vector3 position) // another script - camera extensions
    {

    }
}