using UnityEngine;
using System.Collections;

/*
 * Simple interface implemented by classes that have
 * energy socket info
 */ 

public interface IEnergySocketInfoHandler
{
    EnergySocketInfo info { get; }
}
