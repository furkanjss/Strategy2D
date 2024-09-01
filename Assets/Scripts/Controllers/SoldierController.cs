using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
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
            _model = new SoldierModel(_soldierData);
            Initialize(_model, _view);
            _model.CurrentGrid = transform.parent.GetComponent<GridPiece>();
        }

        protected void Initialize(SoldierModel model, SoldierView view)
        {
            _model = model;
            _view = view;
            _view.Initialize(_model);
        }

        public void MoveToTarget(List<GridPiece> gridPieces)
        {
           
            StartCoroutine(MoveToGridsSequentially(gridPieces));
        }

        private IEnumerator MoveToGridsSequentially(List<GridPiece> targetGridPieces)
        {
            float delayBetweenMoves = 0.1f;

            foreach (var gridPiece in targetGridPieces)
            {
                // Nesneyi hedef grid parçasının altına taşı
                transform.parent = gridPiece.transform;
                transform.localPosition = Vector3.zero;

                // Belirli bir süre bekle
                yield return new WaitForSeconds(delayBetweenMoves);
            }

            // Son hedef grid parçasını güncelle
            _model.CurrentGrid = transform.parent.GetComponent<GridPiece>();
            _model.CurrentGrid.SetSoldierOnGrid(this.gameObject);
        }

    }

}