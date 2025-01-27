using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển

    void Update()
    {
        // Tạo vector di chuyển ban đầu
        Vector3 move = Vector3.zero;

        // Kiểm tra phím nhấn
        if (Input.GetKey(KeyCode.W)) // Nhấn W để đi thẳng
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) // Nhấn S để đi lùi
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A)) // Nhấn A để đi trái
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) // Nhấn D để đi phải
        {
            move += Vector3.right;
        }

        // Di chuyển nhân vật
        transform.parent.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // Xoay nhân vật theo hướng di chuyển
        if (move != Vector3.zero)
        {
            transform.parent.forward = move;
        }
    }
}