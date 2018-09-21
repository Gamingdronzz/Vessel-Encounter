using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI INSTANCE;

    public RawImage CompassImage;


	// Use this for initialization
	void Start ()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else if (INSTANCE != this)
        {
            Destroy(this);
        }
	}

    private void Reset()
    {
        CompassImage = GameObject.Find("CompassImage").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}