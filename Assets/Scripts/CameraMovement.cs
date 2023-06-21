using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    public GameObject character;     
    public GameObject head;     
    private Vector2 mouseLook; 
    private Vector2 smoothV;

    float initialAngle;
    float initialAngleHead;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        character = this.transform.parent.gameObject.transform.parent.gameObject;
        head = this.transform.parent.gameObject;
        initialAngle = character.transform.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, 0f, 0f);

        head.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(initialAngle + mouseLook.x, Vector3.up);
    }
}
