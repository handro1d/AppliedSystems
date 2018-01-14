using System;
using System.Transactions;

namespace AppliedSystems.Common
{
    public class TransactionHelper
    {
        public static void InvokeTransaction(Action action)
        {
            if (Transaction.Current == null)
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        action.Invoke();
                        transaction.Complete();
                    }
                    catch (Exception)
                    {
                        transaction.Dispose();
                        throw;
                    }
                }
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
