using System;
using Models;
using UnityEngine;

namespace Views
{
    public class SoldierView : BaseView<SoldierModel>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Initialize(SoldierModel soldierModel)
        {
            base.Initialize(soldierModel);
        }

        protected override void SetSprite()
        {
            base.SetSprite();
            // Additional setup for soldier sprite if necessary
        }
    }

}