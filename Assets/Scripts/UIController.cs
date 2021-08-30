using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI uiText;

    private GameObject uiPanel;
    private CameraMovementControl cameraMovementControl;
    private Camera mainCamera;


    void Awake()
    {
        mainCamera = Camera.main;
        cameraMovementControl = mainCamera.gameObject.GetComponent<CameraMovementControl>();
        uiPanel = transform.Find("Panel").gameObject;
    }



    public void OpenUI()
    {
        cameraMovementControl.DisableCameraMovement();

        PopulateTextField();

        uiPanel.SetActive(true);
    }

    public void CloseUI()
    {
        cameraMovementControl.EnableCameraMovement();

        uiPanel.SetActive(false);
    }



    private void PopulateTextField()
    {
        //Send ray from left-top corner of camera view port, getting all colliders hit
        Ray ray = mainCamera.ViewportPointToRay(new Vector2(0, 1));
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 50);


        //Going through all hits, looking for one with layer == "backgound"
        //Not required in test task, but still =)
        bool backgroundTileFound = false;
        int i = 0;

        while(!backgroundTileFound && i < hits.Length)
        {

            if (hits[i].transform.gameObject.layer == 6)
            {
                uiText.text = hits[i].transform.name;
                backgroundTileFound = true;
            }

            else
            {
                i++;
            }

        }
        
    }
}
