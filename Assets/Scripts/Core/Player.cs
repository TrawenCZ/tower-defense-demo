using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _buildingKeepDistanceLimit = 5f;

    [SerializeField] private LayerMask _buildingBlockCollisionLayer = new LayerMask();

    [SerializeField] private LayerMask _buildingKeepDistanceLayer = new LayerMask();

    [SerializeField] private int _resources = 500;

    public int Resources
    {
        get { return _resources; }
        set { _resources = value; OnMoneyChanged?.Invoke(_resources); }
    }

    public event Action<int> OnMoneyChanged;

    public void TryPlaceBuilding(Tower buildingToPlace, Vector3 positionToSpawn)
    {
        if (buildingToPlace == null || Resources < buildingToPlace.Price)
        {
            return;
        }

        BoxCollider buildingCollider = buildingToPlace.GetComponent<BoxCollider>();

        if (!CanPlaceBuilding(buildingCollider, positionToSpawn))
        {
            return;
        }

        var building = Instantiate(buildingToPlace, positionToSpawn, Quaternion.identity);

        Resources -= building.Price;
    }

    public bool CanPlaceBuilding(BoxCollider buildingCollider, Vector3 positionToSpawn)
    {
        if (Physics.CheckBox(positionToSpawn + buildingCollider.center, buildingCollider.size / 2, Quaternion.identity, _buildingBlockCollisionLayer))
        {
            return false;
        }

        RaycastHit[] hits = Physics.SphereCastAll(positionToSpawn, _buildingKeepDistanceLimit, Vector3.down, _buildingKeepDistanceLimit, _buildingKeepDistanceLayer, QueryTriggerInteraction.Collide);
        return hits.Count() == 0;
    }
}

