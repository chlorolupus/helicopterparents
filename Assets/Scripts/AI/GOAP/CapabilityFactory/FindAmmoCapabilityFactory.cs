using UnityEngine;
using CrashKonijn.Goap.Runtime;
using CrashKonijn.Goap.Core;
using HelicopterParents.Goap;
using HelicopterParents.Goap.Sensors;

namespace HelicopterParents.Goap.Capabilities
{
    public class FindAmmoCapabilityFactory : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("IdleCapability");

            builder.AddGoal<FindAmmoGoal>()
                .AddCondition<NeedsAmmo>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(2);

            builder.AddAction<FindAmmoAction>()
                .AddEffect<NeedsAmmo>(EffectType.Increase)
                .SetTarget<ClosestAmmo>();

            builder.AddMultiSensor<FindAmmoMultiSensor>();

            return builder.Build();
        }
    }
}