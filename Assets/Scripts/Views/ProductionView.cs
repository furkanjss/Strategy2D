using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionView : MonoBehaviour
{
   private Image _image;
   private ProductionModel _model;

   private void Awake()
   {
      _image = GetComponent<Image>();
   }

   public void Initialize(ProductionModel productionModel)
   {
      _model = productionModel;
      _model.OnStatusChanged += UpdateView;
      UpdateView(_model.GetStatus());
   }

   private void UpdateView(ProductionStatus status)
   {
      switch (status)
      {
         case ProductionStatus.Full:
            SetBuildingSprite();
            break;
         case ProductionStatus.Empty:
            ClearBuildingSprite();
            break;
      }
   }

   private void SetBuildingSprite()
   {
      _image.sprite = _model.GetProductionSprite();
   }

   private void ClearBuildingSprite()
   {
      _image.sprite = null;
   }

   private void OnDestroy()
   {
      _model.OnStatusChanged -= UpdateView;
   }
}
