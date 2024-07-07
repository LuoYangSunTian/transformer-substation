using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cameraMove : MonoBehaviour
{
    public float moveSpeedWithKeyBoard;

    public float moveSpeedWithScroll;

    public float moveSpeedWithMouse;

    public float rotateSpeed;

    //public GameObject target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Rotate();
    }



    /// <summary>
    /// 相机移动
    /// </summary>
    public void Move()
    {

        float moveX = 0;
        float moveY = 0;
        float moveZ = 0;

        // 接收键盘输入
        moveZ = Input.GetAxis("Vertical") * moveSpeedWithKeyBoard * Time.deltaTime;
        moveX = Input.GetAxis("Horizontal") * moveSpeedWithKeyBoard * Time.deltaTime;

        // 接收滚轮输入
        moveZ = Input.GetAxis("Mouse ScrollWheel") * moveSpeedWithScroll * Time.deltaTime;

        // 接收鼠标输入
        if (Input.GetMouseButton(2))
        {
            moveX = -Input.GetAxis("Mouse X") * moveSpeedWithMouse * Time.deltaTime;
            moveY = -Input.GetAxis("Mouse Y") * moveSpeedWithMouse * Time.deltaTime;
        }

        // 控制移动
        transform.position += transform.forward * moveZ + transform.right * moveX + transform.up * moveY;

    }
    /// <summary>
    /// 相机旋转
    /// </summary>
    public void Rotate()
    {
        //接收输入
        float rotateX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        float rotateY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        // 绕自身旋转
        if (Input.GetMouseButton(0))
        {
            Vector3 eulerAngles = transform.eulerAngles;

            eulerAngles.y += rotateX;
            eulerAngles.x -= rotateY;

            transform.eulerAngles = eulerAngles;
        }


        /*// 绕喷头旋转
        if (Input.GetMouseButton(1))
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
     
            transform.position += transform.right * -rotateX + transform.up * -rotateY;
     
            transform.LookAt(target.transform);
     
            transform.position = target.transform.position + -transform.forward * distance;
        }*/
    }
}
