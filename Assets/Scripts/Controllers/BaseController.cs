using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public abstract class BaseController<TModel, TView> : MonoBehaviour
        where TModel : BaseModel
        where TView : BaseView<TModel>
    {
        protected TModel _model;
        protected TView _view;

        protected virtual void Start()
        {
            _view = transform.GetChild(0).GetComponent<TView>();
            _model = CreateModel();
            Initialize(_model, _view);
        }

        protected abstract TModel CreateModel();

        protected void Initialize(TModel model, TView view)
        {
            _model = model;
            _view = view;
            _view.Initialize(_model);
        }

        public virtual void SetInformation()
        {
            InformationPanel.RaiseOnInformationSet(_model);
            Debug.Log(_model);
        }
        public void ApplyDamage(float damage)
        {
            if (_model != null)
            {
                _view.DamageEffect();
                _model.TakeDamage(damage);
                if (_model.GetHealth()<=0)
                {
                    OnDeath();
                }
            }
        }
        public virtual void OnDeath()
        {
        }
    }

}