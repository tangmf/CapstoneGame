using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inworld;

public class CharacterMessager : MonoBehaviour
{
    public InworldCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        character.RegisterLiveSession();
    }
}
