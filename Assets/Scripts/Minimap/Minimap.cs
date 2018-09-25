using UnityEngine;
using VesselEncounter.Data;

namespace VesselEncounter
{
    public class Minimap : SingletonMonoBehaviour<Minimap>
    {
        //public static Minimap INSTANCE;
        public Transform ShipTransform;

        private Transform m_MyTransform;

        //private void Awake()
        //{
        //    if (INSTANCE == null)
        //    {
        //        INSTANCE = this;
        //    }
        //    else if (INSTANCE != this)
        //    {
        //        Destroy(this);
        //    }
        //}

        // Use this for initialization
        private void Start()
        {
            m_MyTransform = transform;
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStateUpdated.EventAction += OnGameStateUpdated;
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnGameStateUpdated.EventAction -= OnGameStateUpdated;
        }

        public void OnGameStateUpdated(object obj)
        {
            XDebug.Log("Game state updated", XDebug.Mask.MiniMap);
            if (GameStateManager.Instance.GetCurrentGameState() == GameStateManager.GameState.Game)
            {
                ShipTransform = GameData.Instance.PlayerGO.transform;
                XDebug.Log("Ship transform attached", XDebug.Mask.MiniMap);
            }
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (GameStateManager.Instance.GetCurrentGameState() == GameStateManager.GameState.Game)
            {
                Vector3 newPos = ShipTransform.position;
                newPos.y = m_MyTransform.position.y;
                m_MyTransform.position = newPos;

                m_MyTransform.rotation = Quaternion.Euler(90.0f, ShipTransform.eulerAngles.y, 0.0f);
            }
        }
    }
}