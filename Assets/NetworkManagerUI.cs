using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using System.Threading.Tasks;

public class NetworkManagerUI : MonoBehaviour
{
    //this may be hosted by the Plane object Floor, for example...

    //do I need to put [SerializeField] before public InputReader Inputs??
    public InputReader Inputs;
    private static bool hasStartedNetworkManager; //false
  

 
    //vivox stuff...

    public async Task InitializeVivoxAsync()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await VivoxService.Instance.InitializeAsync();
    }

    public async Task LoginToVivoxAsync(string userDisplayName)
    {
        LoginOptions options = new LoginOptions();
        options.DisplayName = userDisplayName;
        options.EnableTTS = true;
        await VivoxService.Instance.LoginAsync(options);
    }

    public async Task LeaveEchoChannelAsync()
    {
        string channelToLeave = "Lobby";
        await VivoxService.Instance.LeaveChannelAsync(channelToLeave);
    }

    // end of vivox stuff...



    private void Awake()
    {
    }

    /// <summary>
    /// called every frame
    /// </summary>
    private void Update()
    {
        if (!hasStartedNetworkManager)
        {
            if (Inputs.ButtonA)
            {
                NetworkManager.Singleton.StartHost();
                hasStartedNetworkManager = true;
                startVivoxVoice("host"); //userdisplayname host
            }
            else if (Inputs.ButtonB)
            {
                NetworkManager.Singleton.StartClient();
                hasStartedNetworkManager = true;
                startVivoxVoice("client"); //userdisplayname client
            }
        }
    } //from if hasn't already started network manager host or client

    private async void startVivoxVoice(string userDisplayName)
    {
        await InitializeVivoxAsync();
        await LoginToVivoxAsync(userDisplayName);
        await VivoxService.Instance.JoinEchoChannelAsync("Lobby", ChatCapability.AudioOnly, null);
            //string channelName, ChatCapability chatCapability, ChannelOptions channelOptions = null)
    }

    void OnApplicationQuit()
    {
        Console.WriteLine("quitting...");
        //LeaveEchoChannelAsync();
        //PlayerPrefs.SetString("QuitTime", "The application last closed at: " + System.DateTime.Now);
    }

}
