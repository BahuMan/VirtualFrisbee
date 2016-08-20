using UnityEngine;
using System.Collections;

public class PlayerPreferences : MonoBehaviour {

    public string PlayerName;
    public string PlayerTeam;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
