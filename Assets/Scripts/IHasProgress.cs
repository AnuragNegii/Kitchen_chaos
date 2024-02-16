using System;
using UnityEngine;
using UnityEngine.UI;

public interface IHasProgress {

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs{
        public float progressNormalized;
    }
}