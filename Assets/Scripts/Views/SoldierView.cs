using System;
using Models;
using UnityEngine;

namespace Views
{
    public class SoldierView: MonoBehaviour
    {
        private SoldierModel _model;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(SoldierModel soldierModel)
        {
            if (soldierModel == null)
            {
                throw new ArgumentException(nameof(soldierModel), "Soldier model cannot be null");
            }
            _model = soldierModel;
            SetSoldierSprite();
        }
        private void SetSoldierSprite()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _spriteRenderer.sprite = _model.GetSoldierSprite();
        }
        
    }
}