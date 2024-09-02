using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ProductionController:MonoBehaviour
    {
        [SerializeField] private BuildingData _buildingData;

        private ProductionModel _model;
        private ProductionView _view;
        private Button _productionButton;

        private void Start()
        {
            _view = GetComponent<ProductionView>();
            _productionButton = GetComponent<Button>();
            Initialize(new ProductionModel(_buildingData), _view, _productionButton);
        }

        private void Initialize(ProductionModel model, ProductionView view, Button productionButton)
        {
            _model = model;
            _view = view;
            _productionButton = productionButton;

            _view.Initialize(_model);
            SetupEventListeners();
        }

        private void SetupEventListeners()
        {
            _productionButton.onClick.AddListener(OnProductionButtonClicked);
            _model.OnStatusChanged += HandleStatusChanged;
        }

        private void OnProductionButtonClicked()
        {
            _model.ChangeStatus(ProductionStatus.Empty);
            Vector3 spawnPosition = GetSpawnPosition(); 
            Instantiate(_model.GetBuildPrefab(), spawnPosition, Quaternion.identity);
        }

        private void HandleStatusChanged(ProductionStatus status)
        {
            if (status == ProductionStatus.Empty)
            {
                StartCoroutine(UpdateToFullStatusAfterDelay());
            }
        }

        private IEnumerator UpdateToFullStatusAfterDelay()
        {
            yield return new WaitForSeconds(2);
            _model.ChangeStatus(ProductionStatus.Full);
        }

        private Vector3 GetSpawnPosition()
        {
            return new Vector3(0, -4, 0);
        }

        private void OnDestroy()
        {
            _model.OnStatusChanged -= HandleStatusChanged;
        }
    }
    
}