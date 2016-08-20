using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float turnSpeed = 150f;
    public float horSpeed = 3f;

    public override void OnStartLocalPlayer()
    {
        SmoothFollow cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
        cam.target = this.transform;
    }

	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) return;

        var turn = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
        var mov = Input.GetAxis("Vertical") * Time.deltaTime * horSpeed;

        transform.Rotate(0f, turn, 0f);
        transform.Translate(0, 0, mov);
	
	}
}
