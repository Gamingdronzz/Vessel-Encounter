using UnityEngine;

[RequireComponent (typeof (FloatObject))]
public class ShipController : MonoBehaviour
{
	public Vector3 COM;
	[Space (15)]
	public float Speed = 1.0f;
	public float SteerSpeed = 1.0f;
	public float MovementThresold = 10.0f;

	Transform m_COM;
	float m_VerticalInput;
	float m_MovementFactor;
	float m_HorizontalInput;
	float m_SteerFactor;
    float m_TiltFactor;

	// Update is called once per frame
	void Update ()
    {
		Balance ();
		Movement ();
		Steer ();
	}

	void Balance ()
    {
		if (!m_COM)
        {
			m_COM = new GameObject ("COM").transform;
			m_COM.SetParent (transform);
		}

		m_COM.position = COM;
		GetComponent<Rigidbody> ().centerOfMass = m_COM.position;
	}

	void Movement ()
    {
		m_VerticalInput = Input.GetAxis ("Vertical");
		m_MovementFactor = Mathf.Lerp (m_MovementFactor, m_VerticalInput, Time.deltaTime / MovementThresold);
        transform.Translate (0.0f, 0.0f, m_MovementFactor * Speed);
    }

	void Steer ()
    {
		m_HorizontalInput = Input.GetAxis ("Horizontal");
        m_SteerFactor += SteerSpeed * m_HorizontalInput * Time.deltaTime;
        Tilt(m_SteerFactor);
    }

    void Tilt(float turn)
    {
        if (m_HorizontalInput < 0)
        {
            m_TiltFactor = 10f;
        }
        else if (m_HorizontalInput > 0)
        {
            m_TiltFactor = 350f;
        }
        else
        {
            m_TiltFactor = 0f;
        }

        Quaternion tilt;
        if (m_VerticalInput == 0)
        {
            tilt = Quaternion.Euler(0f, turn, 0f);
        }
        else
        {
            tilt = Quaternion.Euler(0f, turn, m_TiltFactor);
        }
 
        transform.rotation = Quaternion.Lerp(transform.rotation, tilt, Time.deltaTime * 2f);
    }
}