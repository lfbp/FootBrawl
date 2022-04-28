using Unity.Netcode;
using UnityEngine;

public class Menu : NetworkBehaviour
{
    GameObject playerBlue, playerRed;

    void OnGUI() {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) {
            StartButtons();
        }

        GUILayout.EndArea();
    }

    public override void OnNetworkSpawn() {
        if (IsServer) {
            spawnPlayerBlueServerRpc(NetworkManager.Singleton.LocalClientId);
            spawnBallServerRpc(NetworkManager.Singleton.LocalClientId);
        }
        else {
            spawnPlayerRedServerRpc(NetworkManager.Singleton.LocalClientId);
            // spawnBallServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }

    void StartButtons() {
        if (GUILayout.Button("Host")) {
            NetworkManager.Singleton.StartHost();
        }
        if (GUILayout.Button("Client")) {
            NetworkManager.Singleton.StartClient();
        }
    }

    [ServerRpc(RequireOwnership=false)]
    void spawnBallServerRpc(ulong clientId) {
        var myPrefab = (GameObject)Resources.Load("Prefabs/Ball", typeof(GameObject));
        GameObject ball = Instantiate(myPrefab, new Vector3(-1.36f, 0.45f, 0f), Quaternion.identity);
        ball.SetActive(true);
        ball.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership=false)]
    void spawnPlayerBlueServerRpc(ulong clientId) {
        var myPrefab = (GameObject)Resources.Load("Prefabs/Player Blue", typeof(GameObject));
        playerBlue = Instantiate(myPrefab, new Vector2(-3.76f, 1.55f), Quaternion.identity);
        playerBlue.SetActive(true);
        playerBlue.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }

    [ServerRpc(RequireOwnership=false)]
    void spawnPlayerRedServerRpc(ulong clientId) {
        var myPrefab = (GameObject)Resources.Load("Prefabs/Player Red", typeof(GameObject));
        playerRed = Instantiate(myPrefab, new Vector2(1.04f, 1.55f), Quaternion.identity);
        playerRed.SetActive(true);
        playerRed.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }

    [ServerRpc(RequireOwnership=false)]
    void destroyRedPlayerServerRpc() {
        Destroy (playerRed);
    }

    public void ResetPlayersPositions() {
        if (IsServer) {
            Destroy (playerBlue);
            spawnPlayerBlueServerRpc(NetworkManager.Singleton.LocalClientId);
        }
        else {
            destroyRedPlayerServerRpc();
            spawnPlayerRedServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }
}
