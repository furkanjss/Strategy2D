using System;
using Models;
using TMPro;
using UnityEngine;


namespace Views
{
    public class BuildingView: MonoBehaviour
    {
        private Sprite _sprite;
        private BuildingModel _model;
        private TextMeshPro _healthText;
        private void Awake()
        {
            _sprite = transform.GetChild(0).GetComponent<Sprite>();
            _healthText = transform.GetChild(1).GetComponent<TextMeshPro>();

        }

        public void Initialize(BuildingModel buildingModel)
        {
            if (buildingModel == null)
            {
                throw new ArgumentException(nameof(buildingModel), "Building model cannot be null");
            }

            _model = buildingModel;
            _model.OnStatusChanged += UpdateView;
            _model.OnHealthChanged += UpdateHealthView;

            UpdateView(_model.GetStatus());
            UpdateHealthView(_model.GetHealth());
            SetBuildingSprite();
            print(_model.Size);
        }


        private void UpdateView(BuildingStatus status)
        {
            switch (status)
            {
                case BuildingStatus.Available:
                   
                    break;
                case BuildingStatus.Placed:
                 //s   ClearBuildingSprite();
                    break;
            }
        }

        private void UpdateHealthView(float health)
        {
            if (_healthText != null)
            {
                _healthText.text = $"Health: {health}";
            }
        }
       
        private void SetBuildingSprite()
        {
            if (_sprite==null)
            {
                _sprite = transform.GetChild(0).GetComponent<Sprite>();
            }
            _sprite = _model.GetBuildingSprite();
        }
        
        private void OnDestroy()
        {
            _model.OnStatusChanged -= UpdateView;
            _model.OnHealthChanged -= UpdateHealthView;
        }
    }
}