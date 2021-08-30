using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovementControl : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed, minZoom, maxZoom;


    private float targetZoom;
    private bool cameraMovementInProgress;
    private Vector3 panMousePositionStart;


    private Camera mainCamera;



    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        targetZoom = mainCamera.orthographicSize;
    }


    void LateUpdate()
    {
        HandleMousePan();

        HandelZoom();

        RestrictPosition();
    }


    private void HandleMousePan()
    {
        //prevent Camera movement while over UI
        if (EventSystem.current.IsPointerOverGameObject()) return;


        //Start of camera movement
        if (Input.GetMouseButtonDown(0))
        {
            panMousePositionStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            cameraMovementInProgress = true;
        }


        //Move camera while button pressed
        //cameraMovementInProgress required to avoid glitches when mouse clicked over UI

        if (Input.GetMouseButton(0) && cameraMovementInProgress)
        {
            Vector3 movement = panMousePositionStart - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position += movement;
        }
        else
        {
            cameraMovementInProgress = false;
        }

    }



    private void HandelZoom()
    {
        //calculate zoom input
        float wheelDelta = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= wheelDelta * zoomSpeed;

        //change zoom while restricting zoom range
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);

        //restrict targetZoom, so it's not accumulate value over limits
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

    }

    private void RestrictPosition()
    {
        //cashe coordinates of bottom-left and top-right corners of camera's view port
        Vector2 leftBottomCameraCorner = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 rightTopCameraCorner = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));


        //amount of movement to get camera back
        float xMovement = 0, yMovement = 0;

        if (leftBottomCameraCorner.x < MapBuilder.mapBordersCoordinates.Left)
        {
            xMovement = MapBuilder.mapBordersCoordinates.Left - leftBottomCameraCorner.x;
        }

        if (leftBottomCameraCorner.y < MapBuilder.mapBordersCoordinates.Down)
        {
            yMovement = MapBuilder.mapBordersCoordinates.Down - leftBottomCameraCorner.y;
        }

        if (rightTopCameraCorner.x > MapBuilder.mapBordersCoordinates.Right)
        {
            xMovement = MapBuilder.mapBordersCoordinates.Right - rightTopCameraCorner.x;
        }

        if (rightTopCameraCorner.y > MapBuilder.mapBordersCoordinates.Up)
        {
            yMovement = MapBuilder.mapBordersCoordinates.Up - rightTopCameraCorner.y;
        }

        transform.Translate(new Vector3(xMovement, yMovement, 0));
    }

    public void DisableCameraMovement()
    {
        enabled = false;
    }

    public void EnableCameraMovement()
    {
        enabled = true;
    }

}
