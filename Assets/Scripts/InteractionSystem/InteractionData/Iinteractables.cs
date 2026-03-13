using UnityEngine;

public interface IInteractable
{
    string GetPromptText { get; }  //  "Pick Up", "Set at Table", "Set down", Etc 
    void Interact(); //Calls Interaction
}