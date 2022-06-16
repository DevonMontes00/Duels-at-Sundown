using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FishNet.Component.Spawning
{

    /// <summary>
    /// Spawns a team of player objects for clients when they connect.
    /// Must be placed on or beneath the NetworkManager object.
    /// </summary>
    public class TeamSpawner : MonoBehaviour
    {
        #region Public.
        /// <summary>
        /// Called on the server when a player is spawned.
        /// </summary>
        public event Action<NetworkObject> OnSpawned;
        #endregion

        #region Serialized.
        /// <summary>
        /// Prefab to spawn for the player.
        /// </summary>
        [Tooltip("Prefab array to spawn characters for the players.")]
        [SerializeField]
        private NetworkObject [] _playerPrefabs;
        /// <summary>
        /// True to add player to the active scene when no global scenes are specified through the SceneManager.
        /// </summary>
        [Tooltip("True to add player to the active scene when no global scenes are specified through the SceneManager.")]
        [SerializeField]
        private bool _addToDefaultScene = true;
        /// <summary>
        /// Areas in which players may spawn.
        /// </summary>
        [Tooltip("Areas in which players may spawn.")]
        [FormerlySerializedAs("_spawns")]
        public Transform[] Spawns = new Transform[0];
        /// <summary>
        /// Grabs player team to spawn player characters
        /// </summary>
        [SerializeField] private PlayerTeam playerTeam;
        #endregion

        #region Private.
        /// <summary>
        /// NetworkManager on this object or within this objects parents.
        /// </summary>
        private NetworkManager _networkManager;
        /// <summary>
        /// Next spawns to use.
        /// </summary>
        private int _nextSpawn;
        #endregion

        private void Start()
        {
            InitializeOnce();
        }

        private void OnDestroy()
        {
            if (_networkManager != null)
                _networkManager.SceneManager.OnClientLoadedStartScenes -= SceneManager_OnClientLoadedStartScenes;
        }


        /// <summary>
        /// Initializes this script for use.
        /// </summary>
        private void InitializeOnce()
        {
            _networkManager = InstanceFinder.NetworkManager;
            if (_networkManager == null)
            {
                Debug.LogWarning($"PlayerSpawner on {gameObject.name} cannot work as NetworkManager wasn't found on this object or within parent objects.");
                return;
            }

            _networkManager.SceneManager.OnClientLoadedStartScenes += SceneManager_OnClientLoadedStartScenes;
        }

        /// <summary>
        /// Called when a client loads initial scenes after connecting.
        /// </summary>
        private void SceneManager_OnClientLoadedStartScenes(NetworkConnection conn, bool asServer)
        {
            if (!asServer)
                return;
            
            GameObject[] team = playerTeam.GetCharacters();

            for (int i = 0; i < 4; i++)
            {
                if (team[i] == null)
                {
                    Debug.LogWarning($"Player prefab is empty and cannot be spawned for connection {conn.ClientId}.");
                    return;
                }

                Vector3 position;
                Quaternion rotation;
                SetSpawn(team[i].transform, out position, out rotation);

                NetworkObject nob = Instantiate(_playerPrefabs[i], position, rotation);
                _networkManager.ServerManager.Spawn(nob, conn);

                //If there are no global scenes 
                if (_addToDefaultScene)
                    _networkManager.SceneManager.AddOwnerToDefaultScene(nob);

                OnSpawned?.Invoke(nob);
            }
        }


        /// <summary>
        /// Sets a spawn position and rotation.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        private void SetSpawn(Transform prefab, out Vector3 pos, out Quaternion rot)
        {
            //No spawns specified.
            if (Spawns.Length == 0)
            {
                SetSpawnUsingPrefab(prefab, out pos, out rot);
                return;
            }

            Transform result = Spawns[_nextSpawn];
            if (result == null)
            {
                SetSpawnUsingPrefab(prefab, out pos, out rot);
            }
            else
            {
                pos = result.position;
                rot = result.rotation;
            }

            //Increase next spawn and reset if needed.
            _nextSpawn++;
            if (_nextSpawn >= Spawns.Length)
                _nextSpawn = 0;
        }

        /// <summary>
        /// Sets spawn using values from prefab.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        private void SetSpawnUsingPrefab(Transform prefab, out Vector3 pos, out Quaternion rot)
        {
            pos = prefab.position;
            rot = prefab.rotation;
        }

        public NetworkObject GetPlayer(int i)
        {
            return _playerPrefabs[i];
        }

    }


}