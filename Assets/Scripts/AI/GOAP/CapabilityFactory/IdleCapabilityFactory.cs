using UnityEngine;
using CrashKonijn.Goap.Runtime;
using CrashKonijn.Goap.Core;
using HelicopterParents.Goap;
using HelicopterParents.Goap.Sensors;

namespace HelicopterParents.Goap.Capabilities
{
    public class IdleCapabilityFactory : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("IdleCapability");

            builder.AddGoal<IdleGoal>()
                .AddCondition<IsIdle>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(200);

            builder.AddAction<IdleAction>()
                .AddEffect<IsIdle>(EffectType.Increase)
                .SetTarget<IdleTarget>();

            builder.AddTargetSensor<LocalIdleTargetSensor>()
                .SetTarget<IdleTarget>();

            return builder.Build();
        }
    }
}