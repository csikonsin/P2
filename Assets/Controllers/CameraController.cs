using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    public static CameraController Instance { get; protected set; }

	Vector3 currentPosition;
	Vector3 dragOrigin;

	public float minX = -360.0f;
	public float maxX = 360.0f;

	public float minY = -45.0f;
	public float maxY = 45.0f;

	public float sensX = 100f;
	public float sensY = 100f;

    float mouseX = 0.0f;
    float mouseY = 0.0f;
	float rotationY = 0.0f;
	float rotationX = 0.0f;



	float dragSpeed = 3.0f;
    float currDragSpeed;



    void Start () {
        if (Instance != null) { Debug.LogError("There should never be two camera controllers"); }
        Instance = this;
        currDragSpeed = dragSpeed;

        rotationX = transform.localEulerAngles.x;
        rotationY = transform.localEulerAngles.y;
    }

	// Update is called once per frame



	void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");

		currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentPosition.z = 0;

		CameraMovement();
		CameraZoom();


	}
	void CameraMovement()
	{
		if (Input.GetMouseButtonDown(1))
		{
			dragOrigin = Input.mousePosition;
			return;
		}

		if (dragOrigin == null) return;

		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

		if (Input.GetMouseButton(1))
		{
			Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

			float ownY = transform.position.y;
			transform.Translate(move, Space.Self);
			transform.position = new Vector3(transform.position.x, ownY, transform.position.z);
		}
	

		if (Input.GetMouseButton(2))
		{
            float oldX = rotationX;
			rotationX += mouseY * -sensX * Time.deltaTime;
            rotationY += mouseX * sensY * Time.deltaTime;
			transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }

        Vector3 camMovement = Vector3.zero;
        Vector3 camEuler = transform.rotation.eulerAngles;
        camEuler.x = 0f;
        Quaternion normalizedRotation = Quaternion.Euler(camEuler);

        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                camMovement += normalizedRotation * Vector3.left;

            }
            if (Input.GetKey(KeyCode.W))
            {
                camMovement += normalizedRotation * Vector3.forward;

            }
            if (Input.GetKey(KeyCode.S))
            {
                camMovement += normalizedRotation * Vector3.back;

            }
            if (Input.GetKey(KeyCode.D))
            {
                camMovement += normalizedRotation * Vector3.right;

            }

            transform.position +=(camMovement * currDragSpeed * Time.deltaTime);

            currDragSpeed += 1f;
            currDragSpeed = Mathf.Clamp(currDragSpeed, dragSpeed, 30f);
        }
        else
        {
            currDragSpeed = dragSpeed;
        }
       

    }

	    void CameraZoom()
	    {
			    if (Input.GetAxis("Mouse ScrollWheel") != 0)
			    {
				    transform.position += new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * -10,0);
				    if (transform.position.y < 2)
				    {
					    transform.position = new Vector3(transform.position.x, 2, transform.position.z);
				    }
			    }


	    }
	}
