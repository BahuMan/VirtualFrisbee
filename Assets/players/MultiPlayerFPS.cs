using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class MultiPlayerFPS : NetworkBehaviour {

    public GameObject FPSCamera;
    public GameObject model;
    public GameObject disc;
    public float throwForce = 10f;

    private NetworkManager NetworkMgr;

	// Use this for initialization
	void Start () {
        NetworkMgr = GameObject.Find("/VFNetworkManager").GetComponent<NetworkManager>();
        if (NetworkMgr == null) Debug.LogError("Couldn't find NetworkManager!");
	}

    override public void OnStartLocalPlayer()
    {
        GetComponent<RigidbodyFirstPersonController>().enabled = true;
        this.model.SetActive(false);
        Debug.Log("Activitating local camera");
        this.FPSCamera.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate () {
	    if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            CmdFire(transform.position, transform.forward);
        }
	}

    [Command]
    public void CmdFire(Vector3 position, Vector3 direction)
    {
        GameObject newdisc = GameObject.Instantiate(disc);
        newdisc.GetComponent<FrisbeeFlight>().ThrowDisc(position, direction * throwForce);
        NetworkServer.Spawn(newdisc);
    }
}
