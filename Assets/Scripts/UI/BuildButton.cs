using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _representedBuilding;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _buildingName;
    [SerializeField] private LayerMask _floorMask = new LayerMask();

    [SerializeField] private Color _canPlaceColor = new Color();
    [SerializeField] private Color _canNotPlaceColor = new Color();

    private Camera _mainCamera;
    private GameObject _buildingPreviewInstance;
    private Renderer _buildingRendererInstance;
    private BoxCollider _buildingCollider;
    private Player _player;
    private Tower _representedTower;

    private void Start()
    {
        _mainCamera = Camera.main;
        _representedTower = _representedBuilding.GetComponent<Tower>();
        if (_representedTower == null){
            Debug.LogError("Building button does not have a GameObject with Tower script attached");
            return;
        }
        _priceText.text = _representedTower.Price.ToString();
        _buildingName.text = _representedBuilding.name;
        _buildingCollider = _representedTower.Collider;
        _player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_buildingPreviewInstance == null)
        {
            return;
        }

        UpdateBuildingPreview();
    }

    private void UpdateBuildingPreview()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity, _floorMask);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (hasHit)
            {
                _player.TryPlaceBuilding(_representedTower, hit.point);
            }

            Destroy(_buildingPreviewInstance);
            _buildingRendererInstance = null;
        }
        else if (hasHit)
        {
            _buildingPreviewInstance.transform.position = hit.point;

            if (!_buildingPreviewInstance.activeSelf)
            {
                _buildingPreviewInstance.SetActive(true);
            }

            Color color = _player.CanPlaceBuilding(_buildingCollider, hit.point) ? _canPlaceColor : _canNotPlaceColor;

            foreach (Material material in _buildingRendererInstance.materials)
            {
                material.SetColor("_BaseColor", color);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (_player.Resources < _representedTower.Price)
        {
            return;
        }

        _buildingPreviewInstance = Instantiate(_representedTower.BuildingPreview);
        _buildingRendererInstance = _buildingPreviewInstance.GetComponentInChildren<Renderer>();

        _buildingPreviewInstance.SetActive(false);
    }
}
