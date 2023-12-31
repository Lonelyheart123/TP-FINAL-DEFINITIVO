using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void Awake(); 
    void Execute();
    void Sleep();
    IState<T> GetTransition(T input);
    void AddTransition(T input, IState<T> state);
    void RemoveTransition(IState<T> state);
    void RemoveTransition(T input);
}
