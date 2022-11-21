using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public float BoostStrength = 2;
    public float MovementSpeed = 5;
    public bool isActive = false;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isActive)
            return;

        if (Input.GetButton("Fire1"))
            rb.AddForce(Vector3.up * BoostStrength);

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction.Normalize();

        rb.AddForce(direction * MovementSpeed);
    }
}
