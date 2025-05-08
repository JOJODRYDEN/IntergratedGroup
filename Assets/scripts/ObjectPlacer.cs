using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [Header("Object Placement Settings")]
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject previewObjectPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementSurfaceLayerMask;

    [Header("Preview Material Settings")]
    [SerializeField] private Material previewMaterial;
    [SerializeField] private Color validColour;
    [SerializeField] private Color invalidColour;

    [Header("Raycast Settings")]
    [SerializeField] private float objectDistanceFromPlayer;
    [SerializeField] private float raycastStartVerticalOffset;
    [SerializeField] private float raycastDistance;

    private GameObject _previewObject = null;
    private Vector3 _currentPlacementPosition = Vector3.zero;
    private bool _inPlacementMode = false;
    private bool _isValidPlacement = false;

    private void Update()
    { 
        UpdateInput();

        if (_inPlacementMode)
        {
            UpdateCurrentPlacementPosition();

            if (_inPlacementMode)
            {
                UpdateCurrentPlacementPosition();

                if (CanPlaceObject())
                    SetValidPreviewState();
                else
                    SetInvalidPreviewState();
            }
        }
    }

    private void UpdateCurrentPlacementPosition()
    {
        Vector3 cameraForward = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z);
        cameraForward.Normalize();

        Vector3 startPos = playerCamera.transform.position + (cameraForward * objectDistanceFromPlayer);
        startPos.y += raycastStartVerticalOffset;

        RaycastHit hitInfo;
        if (Physics.Raycast(startPos, Vector3.down, out hitInfo, raycastDistance, placementSurfaceLayerMask))
        {
            // Preserve the current Y position while updating X and Z
            _currentPlacementPosition = new Vector3(hitInfo.point.x, _currentPlacementPosition.y, hitInfo.point.z);
        }

        Quaternion rotation = Quaternion.Euler(90f, playerCamera.transform.eulerAngles.y, 0f);
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

            // Adjust object distance using the scroll wheel
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > 0.01f) // Ensure significant input
        {
            objectDistanceFromPlayer += scrollInput * 2f; // Adjust the multiplier as needed
            objectDistanceFromPlayer = Mathf.Clamp(objectDistanceFromPlayer, 1f, 10f); // Clamp to a reasonable range
        }

        // Adjust Y position using Q and E keys
        if (_inPlacementMode)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                _currentPlacementPosition.y -= 0.05f; // Lower the object
            }
            else if (Input.GetKey(KeyCode.E))
            {
                _currentPlacementPosition.y += 0.05f; // Raise the object
            }
        }
    }

    private void SetValidPreviewState()
    {
        previewMaterial.color = validColour;
        _isValidPlacement = true;
    }

    private void SetInvalidPreviewState()
    {
        previewMaterial.color = invalidColour;
        _isValidPlacement = false;
    }

    private bool CanPlaceObject()
    {
        if (_previewObject == null)
            return false;

        return _previewObject.GetComponentInChildren<PreviewValidChecker>().IsValid;
    }

    private void PlaceObject()
    {
        if (!_inPlacementMode || !_isValidPlacement)
            return;

        Quaternion rotation = Quaternion.Euler(90f, playerCamera.transform.eulerAngles.y, 0f);
        Instantiate(placeableObjectPrefab, _currentPlacementPosition, rotation, transform);

        ExitPlacementMode();
    }

    private void EnterPlacementMode()
    {
        if (_inPlacementMode)
            return;

        Quaternion rotation = Quaternion.Euler(90f, playerCamera.transform.eulerAngles.y, 0f);
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
