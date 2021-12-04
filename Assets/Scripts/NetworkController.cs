using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public string servidor;
    public bool isConected = false;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }


    // EVENTO Q RETORNA QUANTO O JOGADOR É CONECTADO
    public override void OnConnected()
    {
        Debug.Log("Conectado");
        isConected = true;
    }


    //EVENTO Q RETORNA QUANDO O JOGADOR FOI CONECTADO E FOI VALIDADO COMO MASTER
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado como Master");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + "   Ping: " + PhotonNetwork.GetPing());

        //criando lobby
        PhotonNetwork.JoinLobby();

    }

    //CONSEGUIU ENTRAR NA SALA
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Entrou no lobby");
    }

    //EVENTO DE ERRO AO ENTRAR SALAS
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomTemp = "Room: " + Random.Range(1,10000);

        PhotonNetwork.CreateRoom(roomTemp);
    }

    //EVENTO DE SUCESSO AO ENTRAR NA SALAs
    public override void OnJoinedRoom()
    {
        Debug.Log("entrou em uma sala: " + PhotonNetwork.CurrentRoom);
    }

    //EVENTO Q RETORNA QUANDO O JOGADOR É DESCONECTADO
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Causa do erro: " + cause);
        PhotonNetwork.ConnectToRegion(servidor);
        isConected = false;
    }
}
