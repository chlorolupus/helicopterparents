using UnityEngine;
using CrashKonijn.Goap.Runtime;
using CrashKonijn.Goap.Core;
using HelicopterParents.Goap;
using HelicopterParents.Goap.Capabilities;

namespace HelicopterParents.Goap.AgentTypes
{
    public class FighterAgent : AgentTypeFactoryBase
    {
        public override IAgentTypeConfig Create()
        {
            var factory = new AgentTypeBuilder("FighterAgent");
            
            factory.AddCapability<IdleCapabilityFactory>();

            return factory.Build();
        }
    }
}
