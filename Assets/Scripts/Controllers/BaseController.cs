using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public abstract class BaseController<TModel, TView> : MonoBehaviour
        where TModel : BaseModel
        where TView : BaseView<BaseModel>
    {
        [SerializeField] private TModel _data;
        protected TModel _model;
        protected TView _view;

        protected virtual void Start()
        {
            _view = transform.GetChild(0).GetComponent<TView>();
            _model = CreateModel(_data);
            Initialize(_model, _view);
        }

        protected abstract TModel CreateModel(TModel data);

        protected void Initialize(TModel model, TView view)
        {
            _model = model;
            _view = view;
            _view.Initialize(_model);
        }

        public void SetInformation()
        {
            InformationPanel.RaiseOnInformationSet(_model);
        }
    }

}