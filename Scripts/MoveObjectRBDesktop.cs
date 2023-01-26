using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObjectRBDesktop : MonoBehaviour {
    public GameObject objectToMove; //object that you want to move - drag in inspector
    public float moveSpeed = 20.0f; //adjust this based on how fast you want your object to move

    private Vector2 movement; 
    private bool TwoD = false;
    private Rigidbody rb;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start(){
        if (objectToMove == null){
            //error reporting
            Debug.Log("Drag the object you want to move into the inspector under 'Object To Move'");
            objectToMove = gameObject;
        }
        if (objectToMove.GetComponent<Rigidbody2D>() != null){
            rb2d = objectToMove.GetComponent<Rigidbody2D>();
            TwoD = true;
        }else if (objectToMove.GetComponent<Rigidbody>() != null){
            rb = objectToMove.GetComponent<Rigidbody>();
        }else{
            Debug.Log("Assign either a RigidBody2D or RigidBody to your player object. Defaulting to RigidBody");
            rb = objectToMove.AddComponent<Rigidbody>();
        }
    }
    void Update(){
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void FixedUpdate(){
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction){
        if (TwoD){
            rb2d.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }else{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }
}
