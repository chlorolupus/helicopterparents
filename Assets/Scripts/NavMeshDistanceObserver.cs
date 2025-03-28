using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using HelicopterParents.Goap.Behaviours;
using HelicopterParents.Goap;
using HelicopterParents.Goap.AgentTypes;
using UnityEngine;
using UnityEngine.AI;

namespace HelicopterParents.Goap.Behaviours{
    public class NavMeshDistanceObserver : MonoBehaviour, IAgentDistanceObserver
    {
        private NavMeshAgent navMeshAgent;  
    
        private void Awake()
        {
            this.navMeshAgent = this.GetComponent<NavMeshAgent>();
            this.GetComponent<BrainBehaviours>().agent.DistanceObserver = this;
        }
    
        public float GetDistance(IMonoAgent agent, ITarget target, IComponentReference reference)
        {
            var distance = this.navMeshAgent.remainingDistance;
        
            // No path
            if (float.IsInfinity(distance))
                return 0f;
            return distance;
        }
    }}