using System;

namespace Utilities
{
    public sealed class Wallet : IWallet
    {
        public int Balance { get; private set; }
        
        public event Action<int, int> BalanceChangedAction;
        public event Action BalanceIsZeroAction;

        public Wallet()
        {
        }

        public Wallet(int balance)
        {
            Balance = balance;
        }

        public void Add(int amount)
        {
            if (amount < 0)
            {
                Spend(-amount);
                return;
            }

            Balance += amount;
            BalanceChangedAction?.Invoke(Balance, amount);
        }

        public void Spend(int amount)
        {
            if (Balance - amount < 0)
            {
                BalanceIsZeroAction?.Invoke();
            }

            Balance -= amount;
            BalanceChangedAction?.Invoke(Balance, -amount);
        }
    }
}