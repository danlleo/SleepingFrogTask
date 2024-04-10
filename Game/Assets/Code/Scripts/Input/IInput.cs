using System;

namespace Input
{
    public interface IInput
    {
        public event Action OnLeftHooked;
        public event Action OnRightHooked;
    }
}