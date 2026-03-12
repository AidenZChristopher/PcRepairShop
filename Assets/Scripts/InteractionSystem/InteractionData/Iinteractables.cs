using UnityEngine;

public interface IInteractable
{
    string GetPromptText { get; }  //  "Pick Up", "Set at Table", "Set down", Etc 
    string GetActionKey { get; }   //  "E", "F"
    void Interact(); //Calls Interaction
}