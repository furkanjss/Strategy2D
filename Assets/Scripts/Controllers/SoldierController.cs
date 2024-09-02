using System;
using System.Collections;
using System.Collections.Generic;

using Models;
using Views;
using UnityEngine;
namespace Controllers
{
    public class SoldierController : BaseController<SoldierModel, SoldierView>
    {
        [SerializeField] private SoldierData _soldierData;
        public GameObject lastTarget;
        protected override SoldierModel CreateModel()
        {
            return new SoldierModel(_soldierData);
        }

        public void MoveToTarget(List<GridPiece> gridPieces, bool isAttack, GridPiece attackGrid = null)
        {
            StartCoroutine(MoveToGridsSequentially(gridPieces, isAttack, attackGrid));
        }

        private IEnumerator MoveToGridsSequentially(List<GridPiece> targetGridPieces, bool isAttack, GridPiece attackGrid = null)
        {
            float delayBetweenMoves = 0.05f;

            foreach (var gridPiece in targetGridPieces)
            {
                transform.parent = gridPiece.transform;
                transform.localPosition = Vector3.zero;
                yield return new WaitForSeconds(delayBetweenMoves);
            }

            _model.CurrentGrid = transform.parent.GetComponent<GridPiece>();
            _model.CurrentGrid.SetSoldierOnGrid(this.gameObject);

            if (isAttack)
            {
               PerformAttack(attackGrid.GetCurrentObject());
               lastTarget =attackGrid.GetCurrentObject();
            }
        }

        public void PerformAttack(GameObject targetObject)
        {
                if (targetObject != null )
                {
                    
                    SoldierController soldier = targetObject.GetComponent<SoldierController>();
                    if (soldier != null)
                    {
                        soldier.ApplyDamage(_model.GetDamage());
                        return;
                    }
                    BuildingController building = targetObject.GetComponent<BuildingController>();
                    if (building != null)
                    {
                        building.ApplyDamage(_model.GetDamage());
                    }
                }
            
        }
        public void SetInformationToPanel()
        {
            InformationPanel.RaiseOnInformationSet(_model);
        }
        
        public override void OnDeath()
        {
            Destroy(gameObject,2);
            transform.parent.GetComponent<GridPiece>().ClearGrid();
        }
    }


}