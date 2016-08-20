using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public float smoothingPosition = 0.01f;
    public float smoothingRotation = 0.01f;
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -2);
    private Camera thisCamera;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (target == null) return;

        Vector3 targetPosition = target.position + target.rotation * offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothingPosition * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothingRotation * Time.deltaTime);
	}
}
