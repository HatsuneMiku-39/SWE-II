using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveShootRBDesktop : MonoBehaviour {
    public GameObject objectToMove; //object that you want to move - drag in inspector
    public float moveSpeed = 20.0f; //adjust this based on how fast you want your object to move
    public GameObject projectilePrefab; //object should have code placed on it that moves the projectile on load
    public GameObject shootFromChildObject; //optional - attach an object you want the projectile to shoot from
    public GameObject projectileParent; //assign projectiles to a parent for easy management (optional)

    private Vector2 movement;
    private bool TwoD = false;
    private Rigidbody rb;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start(){
        if (objectToMove == null){
            //error reporting
            Debug.Log("Drag the object you want to move into the inspector under 'Object To Move'. This defaults to the object the script is placed on if empty");
            objectToMove = gameObject;
        }
        if(shootFromChildObject == null){
            shootFromChildObject = objectToMove;
        }
        if(projectileParent == null){
            projectileParent = new GameObject("ProjectileHolder");
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
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 difference = target - transform.position;

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Fire1")){
            shootBullet();
        }
    }
    void moveCharacter(Vector2 direction){
        if (TwoD){
            rb2d.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }else{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }
    void FixedUpdate(){
        moveCharacter(movement);
    }
    void shootBullet(){
        GameObject projectileClone = Instantiate(projectilePrefab) as GameObject;
        projectileClone.transform.position = shootFromChildObject.transform.position;
        projectileClone.transform.parent = projectileParent.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy"){
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}