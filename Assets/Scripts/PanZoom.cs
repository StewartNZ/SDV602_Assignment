using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    Vector3 objectPosStart;

    RectTransform rectTrans;
    RectTransform parentTrans;

    Vector3 startPos;

    public float zoomInMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        rectTrans = transform.GetComponent<RectTransform>();
        parentTrans = transform.GetComponentInParent<RectTransform>();

        startPos = new Vector3()
        {
            x = 0,
            y = transform.position.y,
            z = 0
        };
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameModel.currentView == GameModel.GameView.Map)
        if (true)
        {
            zoom(Input.GetAxis("Mouse ScrollWheel"));

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

                transform.position = clampToParent(newPos);
            }
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

        transform.position = clampToParent(transform.position);
    }

    Vector3 clampToParent(Vector3 childPos)
    {
        Vector3 parentPos = transform.parent.position;

        Vector3 clamps = new Vector3()
        {
            x = rectTrans.rect.width / 2 * transform.localScale.x - parentTrans.rect.width / 2,
            y = rectTrans.rect.height / 2 * transform.localScale.y - parentTrans.rect.height / 2,
            z = 0
        };

        // 128 is a local space to world space scaler when camera size is set to 5
        clamps = clamps / 128;

        childPos.x = Mathf.Clamp(childPos.x, -clamps.x, clamps.x);
        childPos.y = Mathf.Clamp(childPos.y, -clamps.y + parentPos.y, clamps.y + parentPos.y);

        return childPos;
    }
}
