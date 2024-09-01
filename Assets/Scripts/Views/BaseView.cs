using System;
using System.Collections;
using Models;
using UnityEngine;

namespace Views
{
    public abstract class BaseView<T> : MonoBehaviour where T : BaseModel
    {
        protected T _model;
        protected SpriteRenderer _spriteRenderer;

        protected virtual void Awake()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public virtual void Initialize(T model)
        {
            if (model == null)
            {
                throw new ArgumentException(nameof(model), $"{typeof(T).Name} model cannot be null");
            }

            _model = model;
            _model.OnHealthChanged += UpdateHealthView;

            SetSprite();
            UpdateHealthView(_model.GetHealth());
        }

        protected virtual void SetSprite()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _spriteRenderer.sprite = _model.GetSprite();
        }

        protected virtual void UpdateHealthView(float health)
        {
        }

        protected virtual void OnDestroy()
        {
            _model.OnHealthChanged -= UpdateHealthView;
        }

        public void HighlightInvalidPlacement()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = Color.red;
                StartCoroutine(ResetColorAfterDelay(1f));
            }
        }

        public void DamageEffect()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = Color.red;
                StartCoroutine(ResetColorAfterDelay(1f));
            }
        }
        private IEnumerator ResetColorAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _spriteRenderer.color = Color.white;
        }
    }
}