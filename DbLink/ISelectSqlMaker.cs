using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink
{
    public interface ISelectSqlMaker
    {
        string MakeSelectSql();
        void AddAndCondition(SelectCondition condition);
        void AddOrCondition(SelectCondition condition);
        void AddSelectField(string fieldName);
    }
}
