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
            var builder = new CapabilityBuilder("FindAmmoCapability");

            builder.AddGoal<FindAmmoGoal>()
                .AddCondition<NeedsAmmo>(Comparison.GreaterThanOrEqual, 60)
                .SetBaseCost(1);

            builder.AddAction<FindAmmoAction>()
                .AddEffect<NeedsAmmo>(EffectType.Increase)
                .SetTarget<ClosestAmmo>();

            builder.AddMultiSensor<FindAmmoMultiSensor>();

            return builder.Build();
        }
    }
}