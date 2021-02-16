using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBM;
namespace InvMan
{
    public interface IFixedValues
    {
        string getFixedValue(string valueName);
        void getPeriods();
        List<BalancePosition> getBalancePosition(string positionName);
    }
}
