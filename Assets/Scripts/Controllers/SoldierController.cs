using System;
using Models;
using Views;
using UnityEngine;
namespace Controllers
{
    public class SoldierController : MonoBehaviour
    {
        [SerializeField] private SoldierData _soldierData;
        protected SoldierModel _model;
        protected SoldierView _view;

        protected virtual void Start()
        {
            _view = transform.GetChild(0).GetComponent<SoldierView>();
            SoldierModel model = new SoldierModel(_soldierData);
            Initialize(model, _view);
        }

        protected void Initialize(SoldierModel model, SoldierView view)
        {
            _model = model;
            _view = view;
            _view.Initialize(_model);
        }
        
    }

}