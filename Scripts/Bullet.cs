using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    private Rigidbody2D rb;
    public float bulletSpeed = 60f;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        rb.velocity = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy"){
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
