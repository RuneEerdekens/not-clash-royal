using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlaceUnit : MonoBehaviourPunCallbacks
{

    public InputActionProperty rightTriggerAction;
    public InputDevice device;
    public Material PlaceableMat;
    public Material notPlaceableMat;
    public Material HologramMat;

    public ManaManager ManaMangerScript;

    public ObjectHolder obj;
    public Transform HologramPoint;
    public GameObject RightController;


    private RaycastHit hit;
    private RaycastHit hit2;

    private GameObject Unit;

    private GameObject SelectedObj;
    private MeshRenderer selectedMesh;

    public PhotonView view;

    private GameObject Hull;


    void Start()
    {
        rightTriggerAction.action.performed += spawnUnity;
    }

    private void spawnUnity(InputAction.CallbackContext _obj)
    { 
        if(Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hit, 1000f) && ManaMangerScript.CurrMana > obj.Cost)
        {
            if(hit.collider.tag == "Placable"){
                GameObject tmp = PhotonNetwork.Instantiate(Unit.name, hit.point + new Vector3(0, Unit.transform.localScale.y / 2, 0), Quaternion.identity);
                tmp.GetComponent<PhotonView>().RPC("SetTeamTag", RpcTarget.AllBuffered, tag); // set tag of new object to right team
                ManaMangerScript.RemoveMana(obj.Cost);
            }
        }
    }


    private void FixedUpdate()
    {
        if (!SelectedObj) { return;  }
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
