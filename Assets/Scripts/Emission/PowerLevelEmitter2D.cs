using UnityEngine;
using System;
using System.Collections.Generic;

/*
 * CLASS PowerLevelEmitter2D
 * -------------------------
 * A list of emitters that are assigned certain power levels
 * Clients request to emit with the given power level
 * -------------------------
 */ 

public class PowerLevelEmitter2D : MonoBehaviour, IEmitter
{
    /*
     * PUBLIC TYPEDEFS
     */ 

    [Serializable]
    public class EmitterPowerPair : IComparable<EmitterPowerPair>
    {
        /*
         * PUBLIC DATA
         */

        [SerializeField]
        [Tooltip("Reference to the GameObject with an IEmitter component on it")]
        private GameObject emitterObj;       
        [SerializeField]
        [Tooltip("Power level of this emitter")]
        private int _powerLevel;

        /*
         * GETTERS/SETTERS
         */

        public int powerLevel { get { return _powerLevel; } }
        public IEmitter emitter { get; private set; }

        /*
         * PUBLIC INTERFACE
         */

        public void Initialize()
        {
            emitter = emitterObj.GetComponent<IEmitter>();
        }

        // Implement compare to by comparing by power levels
        public int CompareTo(EmitterPowerPair pair)
        {
            if(_powerLevel < pair._powerLevel)
            {
                return -1;
            }
            else if(_powerLevel > pair._powerLevel)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Emitters that represent each level of the emitter")]
    private List<EmitterPowerPair> emitters;
    [SerializeField]
    [Tooltip("Stockpile that represents the power level of the emitter")]
    private Stockpile powerLevel;

    /*
     * PUBLIC INTERFACE
     */

    public void Emit(Vector2 aimVector)
    {
        EmitterPowerPair emitterPairSelected = null;
        int index = 0;

        // Loop until an emitter pair is select, or the list is exhausted
        while(emitterPairSelected == null && index < emitters.Count)
        {
            // If this is not the last emitter, check to see if the current power level
            // is between this and the next emitter
            if(index < emitters.Count - 1)
            {
                if(powerLevel.currentStock >= emitters[index].powerLevel &&
                    powerLevel.currentStock < emitters[index + 1].powerLevel)
                {
                    emitterPairSelected = emitters[index];
                }   
            }
            // If this is the last emitter and the current power level exceeds its power, select it
            else if (powerLevel.currentStock >= emitters[index].powerLevel)
            {
                emitterPairSelected = emitters[index];
            }
        }

        // If an emitter pair was found, emit it
        if(emitterPairSelected != null)
        {
            emitterPairSelected.emitter.Emit(aimVector);
        }
    }

    /*
     * PRIVATE HELPERS
     */

    // Make sure emitters are sorted by power levels 
    // and properly initialized
    private void Start()
    {
        emitters.Sort();

        foreach(EmitterPowerPair pair in emitters)
        {
            pair.Initialize();
        }
    }
}
