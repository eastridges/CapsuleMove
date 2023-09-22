using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    //this may be hosted by the Plane object Floor, for example...

    //do I need to put [SerializeField] before public InputReader Inputs??
    public InputReader Inputs;
    private bool hasStartedNetworkManager; //false

    private void Awake()
    {
    }

    /// <summary>
    /// called every frame
    /// </summary>
    private void Update()
    {
        if (!hasStartedNetworkManager)
        { }
        if (Inputs.ButtonA)
        {
            NetworkManager.Singleton.StartHost();
            hasStartedNetworkManager = true;
        }
        else if (Inputs.ButtonB)
        {
            NetworkManager.Singleton.StartClient();
            hasStartedNetworkManager = true;
        }
    } //from if hasn't already started network manager host or client

}
