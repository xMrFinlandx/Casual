using System;

namespace Utilities
{
    public interface IWallet
    {
        public int Balance { get; }
        
        public event Action<int, int> BalanceChangedAction;
        public event Action BalanceIsZeroAction;
    }
}