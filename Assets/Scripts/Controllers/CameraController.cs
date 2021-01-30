using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetPlayer;

    public Vector3 offset;
    public float zoomSpeed = 1f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    private float currentZoom = 3.5f;
    public float pitch = 2f;
    private float yawSpeed = 80f;
    private float currentYaw = 0f;

    void Update(){
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetPlayer.position - offset * currentZoom;
        transform.LookAt(targetPlayer.position + Vector3.up * pitch);

        transform.RotateAround(targetPlayer.position, Vector3.up, currentYaw);
    }
}
