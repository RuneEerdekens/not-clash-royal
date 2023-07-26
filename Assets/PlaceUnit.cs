using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceUnit : MonoBehaviour
{

    public InputActionProperty rightTriggerAction;
    public Material PlaceableMat;
    public Material notPlaceableMat;
    public Material HologramMat;

    public ManaManager ManaMangerScript;

    public ObjectHolder obj;
    public Transform HologramPoint;
    public GameObject RightController;

    public LayerMask placeableMask;

    private RaycastHit hit;
    private RaycastHit hit2;

    public int MyTeam;

    private GameObject Unit;

    private GameObject SelectedObj;
    private MeshRenderer selectedMesh;

    // Start is called before the first frame update
    void Start()
    {
        rightTriggerAction.action.performed += spawnUnity;

        ManaMangerScript.Cost = obj.Cost;
        Unit = obj.Object;
        SelectedObj = Instantiate(obj.ObjectHull, Vector3.zero, Quaternion.identity);
        selectedMesh = SelectedObj.GetComponent<MeshRenderer>();
    }

    private void spawnUnity(InputAction.CallbackContext obj)
    { 
        if(Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit, 1000f, placeableMask))
        {
            GameObject tmp = Instantiate(Unit, hit.point + new Vector3(0, Unit.transform.localScale.y/2, 0), Quaternion.identity);
            tmp.GetComponent<HealthScript>().Team = MyTeam;
            tmp.layer = gameObject.layer;
        }
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit2))
        {
            SelectedObj.transform.position = hit2.point + new Vector3(0, SelectedObj.transform.localScale.y/2, 0);
            if (((1<<hit2.collider.gameObject.layer) & placeableMask) != 0 && selectedMesh.material != PlaceableMat) // ????????????????????????????????????????? who made this shit
            {
                selectedMesh.material = PlaceableMat;
            }
            else if(selectedMesh.material != notPlaceableMat)
            {
                selectedMesh.material = notPlaceableMat;
            }
        }
        else
        {
            if(selectedMesh.material != HologramMat)
            {
                selectedMesh.material = HologramMat;
            }
            SelectedObj.transform.position = HologramPoint.position;
        }

    }

    private void OnDestroy()
    {
        rightTriggerAction.action.performed -= spawnUnity;
    }
}
