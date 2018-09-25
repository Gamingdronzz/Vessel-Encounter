using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookRotation : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes Axes = RotationAxes.MouseXAndY;
    public float SensitivityX = 15F;
    public float SensitivityY = 15F;

    public float MinimumX = -360F;
    public float MaximumX = 360F;

    public float MinimumY = -60F;
    public float MaximumY = 60F;

    private float m_RotationX = 0F;
    private float m_RotationY = 0F;

    private List<float> m_RotArrayX = new List<float>();
    private float m_RotAverageX = 0F;

    private List<float> m_RotArrayY = new List<float>();
    private float m_RotAverageY = 0F;

    public float FrameCounter = 20;

    private Quaternion m_OriginalRotation;
    private Transform m_MyTransform;

    private void Start()
    {
        m_MyTransform = transform;
        m_OriginalRotation = m_MyTransform.localRotation;
    }

    private void Update()
    {
        if (Axes == RotationAxes.MouseXAndY)
        {
            m_RotAverageY = 0f;
            m_RotAverageX = 0f;

            m_RotationY += Input.GetAxis("Mouse Y") * SensitivityY;
            m_RotationX += Input.GetAxis("Mouse X") * SensitivityX;

            m_RotArrayY.Add(m_RotationY);
            m_RotArrayX.Add(m_RotationX);

            if (m_RotArrayY.Count >= FrameCounter)
            {
                m_RotArrayY.RemoveAt(0);
            }
            if (m_RotArrayX.Count >= FrameCounter)
            {
                m_RotArrayX.RemoveAt(0);
            }

            for (int j = 0; j < m_RotArrayY.Count; j++)
            {
                m_RotAverageY += m_RotArrayY[j];
            }
            for (int i = 0; i < m_RotArrayX.Count; i++)
            {
                m_RotAverageX += m_RotArrayX[i];
            }

            m_RotAverageY /= m_RotArrayY.Count;
            m_RotAverageX /= m_RotArrayX.Count;

            m_RotAverageY = ClampAngle(m_RotAverageY, MinimumY, MaximumY);
            m_RotAverageX = ClampAngle(m_RotAverageX, MinimumX, MaximumX);

            Quaternion yQuaternion = Quaternion.AngleAxis(m_RotAverageY, Vector3.left);
            Quaternion xQuaternion = Quaternion.AngleAxis(m_RotAverageX, Vector3.up);

            m_MyTransform.localRotation = m_OriginalRotation * xQuaternion * yQuaternion;
        }
        else if (Axes == RotationAxes.MouseX)
        {
            m_RotAverageX = 0f;

            m_RotationX += Input.GetAxis("Mouse X") * SensitivityX;

            m_RotArrayX.Add(m_RotationX);

            if (m_RotArrayX.Count >= FrameCounter)
            {
                m_RotArrayX.RemoveAt(0);
            }
            for (int i = 0; i < m_RotArrayX.Count; i++)
            {
                m_RotAverageX += m_RotArrayX[i];
            }
            m_RotAverageX /= m_RotArrayX.Count;

            m_RotAverageX = ClampAngle(m_RotAverageX, MinimumX, MaximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(m_RotAverageX, Vector3.up);
            m_MyTransform.localRotation = m_OriginalRotation * xQuaternion;
        }
        else
        {
            m_RotAverageY = 0f;

            m_RotationY += Input.GetAxis("Mouse Y") * SensitivityY;

            m_RotArrayY.Add(m_RotationY);

            if (m_RotArrayY.Count >= FrameCounter)
            {
                m_RotArrayY.RemoveAt(0);
            }
            for (int j = 0; j < m_RotArrayY.Count; j++)
            {
                m_RotAverageY += m_RotArrayY[j];
            }
            m_RotAverageY /= m_RotArrayY.Count;

            m_RotAverageY = ClampAngle(m_RotAverageY, MinimumY, MaximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(m_RotAverageY, Vector3.left);
            m_MyTransform.localRotation = m_OriginalRotation * yQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}