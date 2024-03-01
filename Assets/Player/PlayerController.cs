using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift;     // �޸��� Ű
    [SerializeField]
    private KeyCode keyCodeJump = KeyCode.Space;        // ���� Ű
    [SerializeField]
    private KeyCode KeyCodeReload = KeyCode.R;          // ź�� ������ Ű

    private RotateToMouse rotateToMouse;                // ���콺 �̵����� ī�޶� ȸ��
    private MovementCharacterController movement;       // Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status;                              // �̵��ӵ� ���� �÷��̾� ����
    private PlayerAnimatorController animator;         // �ִϸ��̼� ��� ����
    private RifleAudio weapon;                          // ���⸦ �̿��� ���� ����

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotateToMouse = GetComponent<RotateToMouse>();
        movement = GetComponent<MovementCharacterController>();
        status = GetComponent<Status>();
        animator = GetComponent<PlayerAnimatorController>();
        weapon = GetComponentInChildren<RifleAudio>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
        UpdateJump();
        UpdateWeaponAction();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // �̵����� �� (�ȱ� or �ٱ�)
        if (x != 0 || z != 0)
        {
            bool isRun = false;

            // ���̳� �ڷ� �̵��� ���� �޸� �� ����
            if (z > 0) isRun = Input.GetKey(keyCodeRun);

            movement.MoveSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
            animator.MoveSpeed = isRun == true ? 1 : 0.5f;
        }

        // ���ڸ��� �������� ��
        else
        {
            movement.MoveSpeed = 0;
            animator.MoveSpeed = 0;
        }

        movement.MoveTo(new Vector3(x, 0, z));
    }

    private void UpdateJump()
    {
        if (Input.GetKey(keyCodeJump))
        {
            movement.Jump();
        }
    }

    private void UpdateWeaponAction()
    {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartWeaponAction();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                weapon.StopWeaponAction();
            }

            if (Input.GetKeyDown(KeyCodeReload))
            {
                weapon.StartReload();
            }
    }
}
