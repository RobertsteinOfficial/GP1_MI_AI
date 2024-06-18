using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float smooth;
    [SerializeField] private Material material;

    private CharacterController controller;

    public Vector3 moveVector;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        moveVector = new Vector3(xMove, 0, yMove);



        if (moveVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, smooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);



            controller.Move(transform.forward * speed * Time.deltaTime);
        }

    }
}
