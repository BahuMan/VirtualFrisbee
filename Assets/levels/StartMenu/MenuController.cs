using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour {

    [System.Serializable]
    public struct MyStruct
    {
        //putting all links to another component inside a struct, so in the editor I can fold it away
        public InputField PlayerNameInput;
        public Dropdown TeamDropdown;
        public InputField HostnameInput;
        public Button HostButton;
        public Button JoinButton;

        public NetworkManager VFNetworkManager;

    }
    public MyStruct links;

    // Use this for initialization
    void Start () {
        links.HostnameInput.onEndEdit.AddListener(OnHostNameEntered);
        links.HostButton.onClick.AddListener(OnHostButtonClicked);
        links.JoinButton.onClick.AddListener(OnJoinButtonClicked);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnHostNameEntered(string newhostname)
    {
        links.VFNetworkManager.networkAddress = newhostname;
    }

    private void OnHostButtonClicked()
    {
        links.VFNetworkManager.StartHost();
    }

    private void OnJoinButtonClicked()
    {
        links.VFNetworkManager.StartClient();
    }
}
