using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public GameObject lantern;
    public Camera playerCam;
    public float speed = 7.5f;
    public float panSpeed = 2f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public Transform playerCameraParentParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    private bool isPlanningMode;

 //   public GameObject goingTo;

    public List<GameObject> SelectedUnits;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;



    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {

        Movement();
        if (Input.GetKeyDown(KeyCode.B))
        {
            EnterPlanning();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            lantern.SetActive(!lantern.activeInHierarchy);
        }
    }

    public void EnterPlanning()
    {
        if (isPlanningMode)
        {
            playerCameraParentParent.position =transform.position;
            playerCam.GetComponent<PlaceBuilding>().isBuildMode = false;
            playerCam.GetComponent<PlaceBuilding>().canBuild = false;
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            playerCameraParentParent.position = new Vector3(transform.position.x, 250, transform.position.z);
            playerCam.GetComponent<PlaceBuilding>().canBuild = true;
        }
        isPlanningMode = !isPlanningMode;
    }


    public void Movement()
    {
        if (!isPlanningMode)
        { 
            //Code is from https://sharpcoderblog.com/blog/third-person-camera-in-unity-3d
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate move direction based on axes
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                if (Input.GetButton("Jump") && canMove)
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
                playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
                transform.eulerAngles = new Vector2(0, rotation.y);
            }
        }
        else {
            Vector3 forward = playerCameraParent.transform.TransformDirection(Vector3.forward);
            Vector3 right = playerCameraParent.transform.TransformDirection(Vector3.right);
            float curSpeedX = panSpeed * Input.GetAxis("Vertical");
            float curSpeedY = panSpeed * Input.GetAxis("Horizontal");
       
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = panSpeed;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection.y = -panSpeed;
            }
            playerCameraParentParent.transform.Translate(moveDirection);// += new Vector3(moveDirection.x,0,moveDirection.y);
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            if (playerCam.GetComponent<PlaceBuilding>().isBuildMode == false)
            {
                playerCameraParent.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
            }
            //transform.eulerAngles = new Vector2(0, rotation.y);
        }

    }
      
}
