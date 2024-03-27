using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject player;


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Vector3 spawnPosition = new Vector3(0, 5, 0);
        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(player);
    }
}
