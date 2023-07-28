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


    private RaycastHit hit;
    private RaycastHit hit2;

    public int MyTeam;

    private GameObject Unit;

    private GameObject SelectedObj;
    private MeshRenderer selectedMesh;

    private GameObject Hull;

    // Start is called before the first frame update
    void Start()
    {
        rightTriggerAction.action.performed += spawnUnity;
    }

    private void spawnUnity(InputAction.CallbackContext _obj)
    { 
        if(Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit, 1000f) && ManaMangerScript.CurrMana > obj.Cost)
        {
            if(hit.collider.tag == "Placable"){
                GameObject tmp = Instantiate(Unit, hit.point + new Vector3(0, Unit.transform.localScale.y / 2, 0), Quaternion.identity);
                tmp.GetComponent<HealthScript>().Team = MyTeam;
                tmp.layer = gameObject.layer;
                ManaMangerScript.RemoveMana(obj.Cost);
            }
        }
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit2))
        {
            if(hit2.collider.tag != "noDisplay")
            {
                SelectedObj.transform.position = hit2.point + new Vector3(0, SelectedObj.transform.localScale.y / 2, 0);
            } 
            else if(hit2.collider.tag == "noDisplay")
            {
                SelectedObj.transform.position = HologramPoint.position;
            }

            
            if (hit2.collider.tag == "Placable")
            {
                if(ManaMangerScript.CurrMana >= obj.Cost && selectedMesh.material != PlaceableMat)
                {
                    selectedMesh.material = PlaceableMat;
                }
                else if(selectedMesh.material != notPlaceableMat)
                {
                    selectedMesh.material = notPlaceableMat;
                }
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
        if (Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit2,1000f))
        {
            if(hit2.collider.tag == "Placable")
            ManaMangerScript.UpdateCost(obj.Cost);
        }
        else
        {
            ManaMangerScript.UpdateCost(0);
        }

    }

    public void SelectNew(ObjectHolder newObj)
    {
        if (obj)
        {
            Destroy(Hull);
        }

        obj = newObj;
        ManaMangerScript.UpdateCost(newObj.Cost);
        Unit = obj.Object;
        Hull = SelectedObj = Instantiate(newObj.ObjectHull, Vector3.zero, Quaternion.identity);
        selectedMesh = SelectedObj.GetComponent<MeshRenderer>();
    }

    private void OnDestroy()
    {
        rightTriggerAction.action.performed -= spawnUnity;
    }
}
