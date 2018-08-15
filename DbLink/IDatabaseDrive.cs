using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DbLink
{
    public interface IDatabaseDrive
    {
        void ExecuteInsert(string insertSql);
        void ExecuteUpdate(string updateSql);
        void ExecuteDelete(string deleteSql);
        DataSet ExecuteSelect(string selectSql);
    }
}
