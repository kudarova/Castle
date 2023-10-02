using UnityEngine;

class BuildWindow : MonoBehaviour // CHANGE
{
    private GameObject buildWindow;

    private void Start()
    {
        buildWindow = GameObject.FindGameObjectWithTag("Build");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) Debug.Log("2222"); // TriggerBuildMode();
    }

    private void TriggerBuildMode() => buildWindow.SetActive(!buildWindow.activeSelf);
}