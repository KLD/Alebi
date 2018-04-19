﻿using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Configuration;
using ChainedRam.Core.Extentions;

namespace ChainedRam.Core.Generation
{
    public class ProjectileGenerator : QueueInstanceGenerator<Projectile>
    {
        [Header("Projectile Generator")]
        public Projection.Motion MotionOverride;

        public override void SetupGenerated(Projectile generated)
        {
            base.SetupGenerated(generated);

            if (MotionOverride != null)
            {
                var oldMotion = generated.Motion; 
                generated.Motion = MotionOverride.CopyTo(generated.gameObject);

                if (oldMotion != null)
                {
                    Destroy(oldMotion);
                }
            }

            ConfigureManager.ConfigureGameObject(generated.gameObject);
            generated.Setup(Delta);
        }
    }
}