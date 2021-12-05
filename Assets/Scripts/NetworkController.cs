using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public string servidor;
    public bool isConected = false;
    public GameObject Login;
    public GameObject Partidas;
    public InputField usuario;
    public Text usuarioNome;
    public InputField nomeSala;

    string m_nomeUsuario = string.Empty;
    [HideInInspector]
    public string nomeUsuario
    {
        get => m_nomeUsuario;
        set
        {
            m_nomeUsuario = value.Trim();
            usuario.text = m_nomeUsuario;
        }

    }

    string m_nomeSala = string.Empty;
    [HideInInspector]
    public string NomeSala
    {
        get => m_nomeSala;
        set
        {
            m_nomeSala = value;
            nomeSala.text = m_nomeSala;
        }
    }
    // Start is called before the first frame update
    void Start()
    {


    }
    public void Logar()
    {
        if (usuario.text != string.Empty)
        {
            Login.SetActive(false);
            Partidas.SetActive(true);
            usuarioNome.text = nomeUsuario;
            PhotonNetwork.ConnectUsingSettings();

        }
        else
        {
            Debug.Log("Sem nome de Usuário");
        }

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado como master");
        CriarLobby();
    }
    public void CriarLobby()
    {
        

        OnJoinedLobby();
    }
    //CONSEGUIU ENTRAR NO LOBBY
    public override void OnJoinedLobby()
    {
        Debug.Log("Entrou no lobby");
    }

    public void BuscarPartida()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    //EVENTO DE ERRO AO ENTRAR SALAS
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Não existe nenhuma sala");
    }

    public void CriarSala()
    {
        PhotonNetwork.CreateRoom(nomeSala.text);
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


