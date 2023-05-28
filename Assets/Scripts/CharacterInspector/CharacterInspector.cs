using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInspector : MonoBehaviour
{
    public Camera cam;
    public LayerMask layerMask;

    public RectTransform inspectorPanel;
    public RectTransform inspectorPanelChild;
    public bool panelEnabled;
    public Transform selectedCharacter;

    public TMP_Text dataContentText;

    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                CharacterCensusData characterCensusData = hit.collider.GetComponent<CharacterCensusData>();
                if(characterCensusData != null)
                {
                    ToggleInspectorPanel(true);
                    selectedCharacter = characterCensusData.transform;
                    dataContentText.text = characterCensusData.GetMultilineCensusDataString();
                }
            }
        }

        if(selectedCharacter != null && panelEnabled)
        {
            inspectorPanel.anchoredPosition = cam.WorldToScreenPoint(selectedCharacter.position);
            if (inspectorPanel.anchoredPosition.x > Screen.width/2) {
                inspectorPanelChild.anchoredPosition = new Vector2(-327, 321);
            }
            else
            {
                inspectorPanelChild.anchoredPosition = new Vector2(327, 321);
            }
        }
    }

    public void ToggleInspectorPanel(bool toggle)
    {
        panelEnabled = toggle;
        inspectorPanel.gameObject.SetActive(toggle);
    }
}
