using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchboard : MonoBehaviour {

    // All jacks on the switchboard
    private List<Jack> _jacks;
    // Jack.Id | Jack
    private Dictionary<int, Jack> _freeJacks; 
    private Dictionary<int, Jack> _fullJacks;
    private Dictionary<int, Jack> _targetedJacks;

    #region Access Variables
    public List<Jack> Jacks { get { return _jacks; } }
    public Dictionary<int, Jack> EmptyJacks { get { return _freeJacks; } }
    public Dictionary<int, Jack> FullJacks { get { return _fullJacks; } }
    #endregion


    #region Unity Callbacks
    private void Start()
    {
        _jacks = new List<Jack>();
        _freeJacks = new Dictionary<int, Jack>();
        _fullJacks = new Dictionary<int, Jack>();
        _targetedJacks = new Dictionary<int, Jack>();

        foreach(Jack j in _jacks)
        {
            _freeJacks.Add(j.Id, j);
        }
    }
    #endregion

    // Tag jack as full
    public bool FillJack(Jack target)
    {
        // Make sure the jack is on this board
        if(_jacks.Contains(target))
        {
            // Remove jack from empty list
            _freeJacks.Remove(target.Id);
            // Dupe check
            if(!_fullJacks.ContainsKey(target.Id))
            {
                _fullJacks.Add(target.Id, target);
            }
            else 
            {
                Debug.LogWarning("Jack already tagged as full");
            }
            return true;
        }
        else
        {
            Debug.LogError("Jack not found on board.");
        }
        return false;
    }

    // Tag jack as free
    public bool FreeJack(Jack target)
    {
        // Make sure the jack is on this board
        if (_jacks.Contains(target))
        {
            // Remove from full jacks
            _fullJacks.Remove(target.Id);
            // Dupe check
            if(!_freeJacks.ContainsKey(target.Id))
            {
                _freeJacks.Add(target.Id, target);
            }
            else
            {
                Debug.LogWarning("Jack already tagged as empty");
            }
            return true;
        }
        else
        {
            Debug.LogError("Jack not found on board.");
        }
        return false;
    }

    /// <summary>
    /// Return a random available jack
    /// </summary>
    /// <returns></returns>
    public Jack FindFreeJack()
    {
        if(_freeJacks.Count > 0)
        {
            int idx = Random.Range(0, _freeJacks.Count);
            List<Jack> tJacks = new List<Jack>(_freeJacks.Values);
            return tJacks[idx];
        }
        else
        {
            Debug.LogError("No empty jacks available.");
            return null;
        }
        
    }

    // Tag jack as targeted
    public void TargetJack(Jack target)
    {
        _freeJacks.Remove(target.Id);

        // Dupe check
        if(!_targetedJacks.ContainsKey(target.Id))
            _targetedJacks.Add(target.Id, target);
        
    }

    // Untarget a jack
    public void UntargetJack(Jack target)
    {
        _targetedJacks.Remove(target.Id);

         // Free or fill accordingly
        if (target.IsFree)
            FreeJack(target);
        else
            FillJack(target);
    }
}
