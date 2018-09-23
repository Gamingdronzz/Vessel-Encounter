using UnityEngine;
using System.Collections;

class WorldCircle: MonoBehaviour
{
	//private members
	private int m_Segments;
	private float m_XRadius;
	private float m_YRadius;
	private LineRenderer m_Renderer;

	#region Constructors
	// This one does all the work
	public WorldCircle(ref LineRenderer renderer, int segments, float xradius, float yradius)
	{
		m_Renderer = renderer;
		m_Segments = segments;
		m_XRadius = xradius;
		m_YRadius = yradius;
		Draw(segments, m_XRadius, m_YRadius);
	}

	//These are 'convenience' constructors
	public WorldCircle(ref LineRenderer renderer): this(ref renderer, 256, 5.0f, 5.0f) { }

	public WorldCircle(ref LineRenderer renderer, int segments) : this(ref renderer, segments, 5.0f, 5.0f) { }

	public WorldCircle(ref LineRenderer renderer, int segments, float [] radii) : this(ref renderer, segments, radii[0], radii[1]) { }
	#endregion

	public void Draw(int segments, float[] radii)
	{
		m_XRadius = radii[0];
		m_YRadius = radii[1];
		Draw(segments, m_XRadius, m_YRadius);
	}

	public void Draw(int segments, float xradius, float yradius)
	{
		m_XRadius = xradius;
		m_YRadius = yradius;
		m_Renderer.SetVertexCount(segments + 1);
        m_Renderer.useWorldSpace = false;
		CreatePoints();
	}

	public float[] radii
	{
		get {
			float [] values = new float[2];
			values[0] = m_XRadius;
			values[1] = m_YRadius;
			return values;
		}
	}

	private void CreatePoints ()
	{
		float x;
		float z;
		float angle = 20f;

		for (int i = 0; i < (m_Segments + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * m_XRadius;
			z = Mathf.Cos (Mathf.Deg2Rad * angle) * m_YRadius;

			m_Renderer.SetPosition (i,new Vector3(x,0,z) );

			angle += (360f / m_Segments);
		}
	}
}