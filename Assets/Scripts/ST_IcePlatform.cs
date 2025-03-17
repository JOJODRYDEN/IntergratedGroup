using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ST_IcePlatform : MonoBehaviour
{
    [Header("Placement Parameters")]
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject previewObjectPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask placementSurfaceLayerMask;

    [Header("Preview Material")]
    [SerializeField] private Material previewMaterial;
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;

    [Header("Raycast Parameters")]
    [SerializeField] private float objectDistanceFromPlayer;
    [SerializeField] private float raycastStartVerticalOffset;
    [SerializeField] private float raycastDistance;
    [SerializeField] private float scrollSensitivity = 1f;


    private GameObject _previewObject = null;
    private Vector3 _currentPlacementPosition = Vector3.zero;
    private bool _inPlacementMode = false;
    private bool _validPreviewState = false;

    private void Update()
    {
        UpdateInput();

        if (_inPlacementMode)
        {
            UpdateCurrentPlacementPosition();

            if (CanPlaceObject())
                SetValidPreviewState();
            else
                SetInvalidPreviewState();
        }
    }

    private void UpdateCurrentPlacementPosition()
    {
        Vector3 cameraForward = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z);
        cameraForward.Normalize();

        Vector3 startPos = mainCamera.transform.position + (cameraForward * objectDistanceFromPlayer);
        startPos.y += raycastStartVerticalOffset;

        RaycastHit hitInfo;
        if (Physics.Raycast(startPos, Vector3.down, out hitInfo, raycastDistance, placementSurfaceLayerMask))
        {
            _currentPlacementPosition = hitInfo.point;
        }

        Quaternion rotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
        _previewObject.transform.position = _currentPlacementPosition;
        _previewObject.transform.rotation = rotation;
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnterPlacementMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ExitPlacementMode();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }

        // Handle scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            objectDistanceFromPlayer += scrollInput * scrollSensitivity;
            objectDistanceFromPlayer = Mathf.Clamp(objectDistanceFromPlayer, 1f, 10f); // Adjust the min and max values as needed
        } 
    }

    private void SetValidPreviewState()
    {
        previewMaterial.color = validColor;
        _validPreviewState = true;
    }

    private void SetInvalidPreviewState()
    {
        previewMaterial.color = invalidColor;
        _validPreviewState = false;
    }

    private bool CanPlaceObject()
    {
        if (_previewObject == null)
            return false;

        return _previewObject.GetComponentInChildren<ST_PlatformPreview>().IsValid;
    }

    private void PlaceObject()
    {
        if (!_inPlacementMode || !_validPreviewState)
            return;

        Quaternion rotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
        Instantiate(placeableObjectPrefab, _currentPlacementPosition, rotation, transform);

        ExitPlacementMode();
    }

    private void EnterPlacementMode()
    {
        if (_inPlacementMode)
            return;

        Quaternion rotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
        _previewObject = Instantiate(previewObjectPrefab, _currentPlacementPosition, rotation, transform);
        _inPlacementMode = true;
    }

    private void ExitPlacementMode()
    {
        Destroy(_previewObject);
        _previewObject = null;
        _inPlacementMode = false;
    }
}