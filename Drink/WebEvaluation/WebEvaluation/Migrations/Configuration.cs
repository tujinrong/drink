namespace WebEvaluation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebEvaluation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebEvaluation.DAL.EvaluationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebEvaluation.DAL.EvaluationContext context)
        {
            //context.Seed();   
        }
    }
}
