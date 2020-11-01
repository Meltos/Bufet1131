using System;
using System.Collections.ObjectModel;

namespace Bufet1131Vorobyov
{
    internal class AccountingSql
    {
        private DB dB;

        public AccountingSql(DB dB)
        {
            this.dB = dB;
        }

        internal ObservableCollection<Accounting> GetData()
        {
            throw new NotImplementedException();
        }

        internal void EditAccounting(Accounting selectedAccounting)
        {
            throw new NotImplementedException();
        }
    }
}