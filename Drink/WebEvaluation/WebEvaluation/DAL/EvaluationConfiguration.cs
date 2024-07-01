using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace WebEvaluation.DAL
{
    public class EvaluationConfiguration : DbConfiguration
    {
        public EvaluationConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}