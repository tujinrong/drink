using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebEvaluation.Models;

namespace WebEvaluation.DAL
{
    public class EvaluationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EvaluationContext>
    {
        protected override void Seed(EvaluationContext context)
        {

        }
    }
}