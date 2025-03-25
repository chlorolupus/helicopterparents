using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace CrashKonijn.Goap.AIGOAPGenerator.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FighterBehaviour : MonoBehaviour
    {
        public NavMeshAgent agent;
        public float scanRadius;
        public int ammoCount = 0;
        public GameObject target;
        public float ammoRefillTick = 0.05f;
        public float healthRefillTick = 0.05f;
        public int maxHealth = 10;
        public float alertness = 0f;
        public float aggressiveness = 0f;
        public bool isRefillingAmmo = false;
        public bool isRefillingHealth = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [FormerlySerializedAs("ScanRadiusGameObject")]
        public GameObject scanRadiusGameObject;

        void Awake()
        {
            scanRadiusGameObject = transform.Find("Visualizer_ScanRadius").gameObject;
            scanRadius = scanRadiusGameObject.transform.localScale.x / 2;
        }
        
        
        
        void Start()
        {

        }
        
        // Update is called once per frame
        void Update()
        {
            var path = new NavMeshPath();
            agent.CalculatePath(target.transform.position, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    Debug.Log($"{agent.name} will be able to reach {target.name}.");
                    agent.SetDestination(target.transform.position);
                    agent.SetPath(path);
                    break;
                case NavMeshPathStatus.PathPartial:
                    Debug.LogWarning($"{agent.name} will only be able to move partway to {target.name}.");
                    break;
                default:
                    Debug.LogError($"There is no valid path for {agent.name} to reach {target.name}.");
                    break;

            }
        }
    }
}
