using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public InputReader Inputs;
    internal bool InputReaderGameObjectExists;  //false
    internal bool InputReaderComponentExists;  // false


    private void Start()
    {

    }
    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        //assign Inputs again even though was done in the Start() method
        GameObject go = GameObject.FindGameObjectWithTag("InputReaderTag");
        if (go != null)
        {
            InputReaderGameObjectExists = true;
            Inputs = go.GetComponent<InputReader>();
            if (Inputs != null)
            {
                InputReaderComponentExists = true;
            }
        }

        Vector3 moveDir = new Vector3();
        float moveSpeed = 3f;
        float vertSpeed = 0;
        //if (InputReaderComponentExists)  //NOTE This returns true and vert speed is up if not commented out... 
        //    vertSpeed = 0.5f;
        //else
        //    vertSpeed = -0.5f;
        if ((go != null) && (InputReaderComponentExists) && (Inputs.RightControllerFound))
        //if(false)
        {
            //this should be true for the Quest2 headset
            Vector2 joystick = Inputs.rightJoystick;
            moveDir = new Vector3(joystick.x, vertSpeed, joystick.y);
            //if (Inputs.ButtonA)
            //    moveDir.x = +1f;
            //else if (Inputs.ButtonB)
            //    moveDir.x = -1f;
        }
        else
        {
            //this should work for the Windows keyboard 
            if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
            if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
            if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
            if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;


            //the following code works for debugging when now input is found...
            ////////just make some motion to show it's alive
            //////moveSpeed = 1; //slower
            //////int cycle = DateTime.Now.Second;
            //////if (cycle % 4 == 0) //modulus
            //////{
            //////    moveDir.x = +1f;
            //////}
            //////else if (cycle % 4 == 1)
            //////{
            //////    moveDir.z = +1f;
            //////}
            //////else if (cycle % 4 == 2)
            //////{
            //////    moveDir.x = -1f;
            //////}
            //////else
            //////{
            //////    moveDir.z = -1f;
            //////}
        }

        moveDir.y = vertSpeed;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
