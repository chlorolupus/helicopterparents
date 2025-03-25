using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace HelicopterParents.Goap.Behaviours
{
    public class BrainBehaviours : MonoBehaviour
    {        
        private AgentBehaviour agent;
        private GoapActionProvider provider;
        private GoapBehaviour goap;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Awake()
        {
            this.goap = FindFirstObjectByType<GoapBehaviour>(FindObjectsInactive.Include);
            this.agent = this.GetComponent<AgentBehaviour>();
            this.provider = this.GetComponent<GoapActionProvider>();
            
            if (this.provider.AgentTypeBehaviour == null)
                this.provider.AgentType = this.goap.GetAgentType("FighterAgent");
        }
        private void Start()
        {
            this.provider.RequestGoal<IdleGoal>();
        }
    }
}
