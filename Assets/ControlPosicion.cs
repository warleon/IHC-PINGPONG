using UnityEngine;
using Unity.Netcode;

public class ControlPosicion : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        // Solo ejecuta esto si este objeto es MÍO (mi conexión)
        if (IsOwner)
        {
            // Buscamos tus gafas de VR en la escena
            GameObject rig = GameObject.Find("OVRCameraRig");

            if (rig != null)
            {
                // === JUGADOR 1 (Servidor/Host) ===
                // Se queda en el origen (0,0,0) mirando hacia el frente
                if (IsServer)
                {
                    rig.transform.position = new Vector3(0.0f, 0.0f, -0.56f);
                    rig.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                // === JUGADOR 2 (Cliente) ===
                // Se pone en Z 3.13 y gira 180 grados para mirar al Jugador 1
                else
                {
                    // X = 0 (centro), Y = 0 (piso), Z = 3.13 (al frente)
                    rig.transform.position = new Vector3(0.0f, 0.0f, 3.0f);

                    // Giramos 180 grados en Y para que mire hacia atrás (hacia el 0,0,0)
                    rig.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
}