using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity;
    private CinemachineComposer composer;

    void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset.y = 1;
    }

    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, 0, 2);
    }
}
