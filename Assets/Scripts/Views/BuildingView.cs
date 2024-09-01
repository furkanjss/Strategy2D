using System;
using System.Collections;
using Models;
using TMPro;
using UnityEngine;


namespace Views
{
    public class BuildingView : BaseView<BuildingModel>
    {
        private TextMeshPro _healthText;

        protected override void Awake()
        {
            base.Awake();
            _healthText = transform.GetChild(1).GetComponent<TextMeshPro>();
        }

        public override void Initialize(BuildingModel buildingModel)
        {
            base.Initialize(buildingModel);
            _model.OnStatusChanged += UpdateView;
            UpdateView(_model.GetStatus());
        }

        protected override void SetSprite()
        {
            base.SetSprite();
            // Additional setup for building sprite if necessary
        }

        private void UpdateView(BuildingStatus status)
        {
            switch (status)
            {
                case BuildingStatus.Available:
                    // Logic for available status
                    break;
                case BuildingStatus.Placed:
                    // Logic for placed status
                    break;
            }
        }

        protected override void UpdateHealthView(float health)
        {
            if (_healthText != null)
            {
                _healthText.text = $"Health: {health}";
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _model.OnStatusChanged -= UpdateView;
        }
    }

}