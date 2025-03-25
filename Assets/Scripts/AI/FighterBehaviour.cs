using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace HelicopterParents.Goap.Behaviours
{
    public class FighterBehaviour : MonoBehaviour
    {
        [FormerlySerializedAs("agent")] public NavMeshAgent nmAgent;
        public float scanRadius;
        public int ammoCount = 0;
        private AgentBehaviour agent;
        public bool shouldMove = true;
        private ITarget currentTarget;
        [FormerlySerializedAs("target")] public GameObject moveTarget;
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
            this.agent = this.GetComponent<AgentBehaviour>();
        }
        private void OnEnable()
        {
            this.agent.Events.OnTargetInRange += this.OnTargetInRange;
            this.agent.Events.OnTargetChanged += this.OnTargetChanged;
            this.agent.Events.OnTargetNotInRange += this.TargetNotInRange;
            this.agent.Events.OnTargetLost += this.TargetLost;
        }

        private void OnDisable()    
        {
            this.agent.Events.OnTargetInRange -= this.OnTargetInRange;
            this.agent.Events.OnTargetChanged -= this.OnTargetChanged;
            this.agent.Events.OnTargetNotInRange -= this.TargetNotInRange;
            this.agent.Events.OnTargetLost -= this.TargetLost;
        }
        
        private void TargetLost()
        {
            this.currentTarget = null;
            this.shouldMove = false;
        }

        private void OnTargetInRange(ITarget target)
        {
            this.shouldMove = false;
        }

        private void OnTargetChanged(ITarget target, bool inRange)
        {
            this.currentTarget = target;
            this.shouldMove = !inRange;
        }

        private void TargetNotInRange(ITarget target)
        {
            this.shouldMove = true;
        }

        void Start()
        {

        }
        
        // Update is called once per frame
        void Update()
        {
            if (this.agent.IsPaused)
                return;

            if (!this.shouldMove)
                return;
        
            if (this.currentTarget == null)
                return;
        
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.currentTarget.Position.x, this.transform.position.y, this.currentTarget.Position.z), Time.deltaTime*10f);
            
            //  I will do the navmesh part tomorrow on 26/3/2025 (after i am off work)
            var path = new NavMeshPath();
            if (moveTarget != null)
            {
                nmAgent.CalculatePath(moveTarget.transform.position, path);

                switch (path.status)
                {
                    case NavMeshPathStatus.PathComplete:
                        Debug.Log($"{nmAgent.name} will be able to reach {moveTarget.name}.");
                        nmAgent.SetDestination(moveTarget.transform.position);
                        nmAgent.SetPath(path);
                        break;
                    case NavMeshPathStatus.PathPartial:
                        Debug.LogWarning($"{nmAgent.name} will only be able to move partway to {moveTarget.name}.");
                        break;
                    default:
                        Debug.LogError($"There is no valid path for {nmAgent.name} to reach {moveTarget.name}.");
                        break;

                }
            }
        }
    }
}
