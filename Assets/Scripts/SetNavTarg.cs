using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;

public class SetNavigationTarget : MonoBehaviour {
    [SerializeField]
    private TMP_Dropdown navTargetDropDown;
    [SerializeField]
    private List<Target> navTargetObjects = new List<Target>();
    [SerializeField]
    private Slider navYOffset;
    
    private NavMeshPath path; // current calculated path
    private LineRenderer line; // linerenderer to display path
    private Vector3 targetPosition = Vector3.zero; // current target position
    private bool lineToggle = false;

    private void Start(){
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    private void Update() {
        if (lineToggle && targetPosition != Vector3.zero) {
            if(NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path)){
                line.positionCount = path.corners.Length;
                line.SetPositions(path.corners);
                Vector3[] calculatedPathandOffset = AddLineOffset();
                line.SetPositions(calculatedPathandOffset);
            }
            
        }

    }
    public void SetCurrentNavTarget(int selectedValue){
        targetPosition = Vector3.zero;
        string selectedText = navTargetDropDown.options[selectedValue].text;
        Target currentTarget = navTargetObjects.Find(x => x.Name.Equals(selectedText));
        if(currentTarget != null){
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }

    private void CalculatePath(Vector3 start, Vector3 end)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path);

        // Check if the path is valid
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            // Pathfinding was successful
            Debug.Log("Pathfinding successful!");
        }
        else
        {
            // Pathfinding failed
            Debug.LogError("Failed to find a valid path!");
        }
    }

    public void ToggleVisibility(){
        lineToggle = !lineToggle;
        line.enabled = lineToggle;
    }
	
    private Vector3[] AddLineOffset(){
        if(navYOffset.value == 0){
            return path.corners;
        }
        Vector3[] calculatedLine = new Vector3[path.corners.Length];
        for(int i = 0; i < path.corners.Length; i++){
            calculatedLine[i] = path.corners[i] + new Vector3(0, navYOffset.value, 0);
        }
        return calculatedLine;
    }
}



