using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchboard : MonoBehaviour {

    private List<Jack> _jacks;
    private List<Jack> _emptyJacks;
    private List<Jack> _fullJacks;

    #region Access Variables
    public List<Jack> Jacks { get { return _jacks; } }
    public List<Jack> EmptyJacks { get { return _emptyJacks; } }
    public List<Jack> FullJacks { get { return _fullJacks; } }
    #endregion


    #region Unity Callbacks
    private void Start()
    {
        _jacks = new List<Jack>();
        _emptyJacks = new List<Jack>();
        _fullJacks = new List<Jack>();
    }
    #endregion

    // Called by jacks to tag them as full
    public bool FillJack(Jack target)
    {
        // Make sure the jack is on this board
        if(_jacks.Contains(target))
        {
            // Check the list of empty jacks
            if(_emptyJacks.Contains(target))
            {
                _emptyJacks.Remove(target);
                _fullJacks.Add(target);
                return true;
            }
            // If not tagged as empty, see if already tagged as full
            else if (_fullJacks.Contains(target))
            {
                Debug.LogWarning("Jack already tagged as full");
            }
        }
        else
        {
            Debug.LogError("Jack not found on board.");
        }
        return false;
    }
    // Called by jacks to untag them as empty
    public bool FreeJack(Jack target)
    {
        // Make sure the jack is on this board
        if (_jacks.Contains(target))
        {
            // Check the list of empty jacks
            if (_fullJacks.Contains(target))
            {
                _fullJacks.Remove(target);
                _emptyJacks.Add(target);
                return true;
            }
            // If not tagged as empty, see if already tagged as full
            else if (_emptyJacks.Contains(target))
            {
                Debug.LogWarning("Jack already tagged as empty");
            }
        }
        else
        {
            Debug.LogError("Jack not found on board.");
        }
        return false;
    }

    
}
