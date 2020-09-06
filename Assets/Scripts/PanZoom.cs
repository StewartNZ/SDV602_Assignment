using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    Vector3 objectPosStart;
    GameObject parent;

    public float zoomInMax = 5;

    Vector3 origPos;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Touch Started");
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                objectPosStart = transform.position;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 worldMousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = touchStart - worldMousePos;
                Vector3 newPos = objectPosStart - direction;

                //newPos.x = Mathf.Clamp(newPos.x, -100f / 128f, 100f / 128f);
                //newPos.y = Mathf.Clamp(newPos.y, -280f / 128f, -80f / 128f);

                if (transform.localScale.x == 1)
                {
                    newPos.x = Mathf.Clamp(newPos.x, 0, 0);
                    newPos.y = Mathf.Clamp(newPos.y, -180f / 128f, -180f / 128f);
                }
                else
                {
                    float difference = 360f * transform.localScale.x - 360f;
                    newPos.x = Mathf.Clamp(newPos.x, -difference / 128f, difference / 128f);
                    newPos.y = Mathf.Clamp(newPos.y, (-difference - 180f) / 128f, (difference - 180f) / 128f);
                }

                transform.position = newPos;
            }

            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void zoom(float increment)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, 1, zoomInMax);
        Vector3 newScale = new Vector3
        {
            x = Mathf.Clamp(transform.localScale.x + increment, 1, zoomInMax),
            y = Mathf.Clamp(transform.localScale.y + increment, 1, zoomInMax),
            z = 0
        };

        transform.localScale = newScale;

        Vector3 newPos = transform.position;

        if (transform.localScale.x == 1)
        {
            newPos.x = Mathf.Clamp(newPos.x, 0, 0);
            newPos.y = Mathf.Clamp(newPos.y, -180f / 128f, -180f / 128f);
        }
        else
        {
            float difference = 360f * transform.localScale.x - 360f;
            newPos.x = Mathf.Clamp(newPos.x, -difference / 128f, difference / 128f);
            newPos.y = Mathf.Clamp(newPos.y, (-difference - 180f) / 128f, (difference - 180f) / 128f);
        }

        transform.position = newPos;
    }
}
