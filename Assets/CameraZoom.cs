using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public float defaultValue = 1.0f;
    public float min = 0.5f;
    public float max = 5.0f;
    public float scrollRate = 0.5f;
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ZoomIn(scrollRate);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            ZoomOut(scrollRate);
        }
    }

    public void ZoomOut(float x)
    {
        vcam.m_Lens.OrthographicSize += x;
        if (vcam.m_Lens.OrthographicSize >= max)
        {
            vcam.m_Lens.OrthographicSize = max;
        }
    }

    public void ZoomIn(float x)
    {
        vcam.m_Lens.OrthographicSize -= x;
        if (vcam.m_Lens.OrthographicSize <= min)
        {
            vcam.m_Lens.OrthographicSize = min;
        }
    }

    public void ZoomReset()
    {
        vcam.m_Lens.OrthographicSize = defaultValue;
    }
}
