using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{   
   
   
   [SerializeField] private GameObject SoldierProducedButtonPrefab;

   public static event Action<BuildingModel> OnInformationSet;
   public static void RaiseOnInformationSet(BuildingModel buildingModel)
   {
      OnInformationSet?.Invoke(buildingModel);
   }
   [SerializeField] private TextMeshProUGUI buildingName;
   [SerializeField] private TextMeshProUGUI healthText;
   [SerializeField] private Image buildingImage;
   [SerializeField] private Transform buttonsParent;
   private BuildingModel modelInformation;
   public void SetInformation(BuildingModel model)
   {
      if (modelInformation == model)
      {
         Debug.Log("Information already set.");
         return;
      }

      if (modelInformation != null)
      {
         ClearInformation();
      }

      modelInformation = model;

      UpdateBuildingInfo();

      if (modelInformation is BarracksModel barracksModel)
      {
         CreateSoldierButtons(barracksModel);
      }
   }

   private void UpdateBuildingInfo()
   {
      if (modelInformation == null) return;

      buildingImage.sprite = modelInformation.GetBuildingSprite();
      buildingName.text = modelInformation.Name;
      healthText.text = $"Health: {modelInformation.GetHealth()}";

      modelInformation.OnHealthChanged += UpdateHealthView;
   }

   private void CreateSoldierButtons(BarracksModel barracksModel)
   {
      int soldierCount = barracksModel.GetSoldierDatas().Count;
      for (int i = 0; i < soldierCount; i++)
      {
         var soldierButton = Instantiate(SoldierProducedButtonPrefab, buttonsParent);
         soldierButton.transform.localPosition = new Vector3(0, -150 * i, 0);

         var soldierData = barracksModel.GetSoldierDatas()[i];
         soldierButton.GetComponentInChildren<TextMeshProUGUI>().text = soldierData.name;
         soldierButton.transform.GetChild(1).GetComponent<Image>().sprite = soldierData.soldierSprite;
         soldierButton.GetComponent<UnitsButton>().GetSoldierData(barracksModel.GetSoldierDatas()[i]);
      }
   }

   private void OnEnable()
   {
      OnInformationSet += SetInformation;
   }

   void UpdateHealthView(float health)=>     healthText.text = $"Health: {health}";
   void ClearInformation()
   {
      modelInformation.OnHealthChanged -= UpdateHealthView;
      
      
      foreach (Transform child in buttonsParent)
      {
         Destroy(child.gameObject);
      }
   }

   private void OnDisable()
   {
      if (modelInformation!=null)
      {
         modelInformation.OnHealthChanged -= UpdateHealthView;
      }
      OnInformationSet -= SetInformation;

   }
}
