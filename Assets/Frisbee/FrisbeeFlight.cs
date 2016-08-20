using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class FrisbeeFlight : NetworkBehaviour {

    [Tooltip("How much force to add straight down the world's Y-axis")]
    public float gravity;

    [Tooltip("Depending on speed, how much lift to generate along this disc's Y-axis")]
    public float lift;

    [Tooltip("How much the horizontal speed (along the disc's edges) will slow down")]
    public float drag;

    [Tooltip("Without spin, the disc would orient itself to the flight direction and produce minimal lift & drag")]
    public float spin;

    private Rigidbody thisBody;
    private bool airBorne = true;

    void Awake()
    {
        thisBody = GetComponent<Rigidbody>();
        FlattenMesh();
    }

    //ugly trick because I'm too lazy to start Blender and create a disc
    private void FlattenMesh()
    {
        Mesh cyl = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = cyl.vertices;
        for (int i = 0; i < cyl.vertexCount; ++i)
        {
            verts[i] = new Vector3(verts[i].x, verts[i].y * 0.05f, verts[i].z);
        }
        cyl.vertices = verts;
        cyl.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = cyl;
    }

    int counter = 30;
    void FixedUpdate()
    {
        if (!airBorne) return;

        thisBody.AddForce(new Vector3(0, -gravity * Time.fixedDeltaTime, 0));
        //vertical lift is a function of horizontal speed. This is an awful simplification of Bernouilli's laws:
        float liftCoefficient = 1 / (1 - Vector3.Dot(thisBody.velocity.normalized, transform.up));
        thisBody.AddForce(transform.up * lift * liftCoefficient * Time.fixedDeltaTime);
        //thisBody.AddTorque()
        if (--counter <= 0)
        {
            counter = 30;
            Debug.Log("Frisbee velocity = " + thisBody.velocity.magnitude);
        }
    }

    void disabledOnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Picked up by Player");
            thisBody.isKinematic = true;
            transform.parent = other.transform;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Bounce");
        airBorne = false;
        thisBody.useGravity = true;
    }

    public void ThrowDisc(Vector3 position, Vector3 direction) {
        transform.position = position;
        transform.LookAt(position + direction);
        thisBody.isKinematic = false;
        thisBody.velocity = direction;
        airBorne = true;
    }
}
