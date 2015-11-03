using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningBasic.Parsing.Ast.Statements
{
    public class Load : IStatement
    {
        public string Name { get; private set; }

        public Load(string name)
        {
            if (name == null)
                throw new ArgumentNullException("programName");

            Name = name;
        }

        public EvaluateResult Evaluate(IRunTimeEnvironment rte)
        {
            rte.Load(Name);

            var message = string.Format(Messages.ProgramLoaded, rte.LastUsedName);
            return new EvaluateResult(message);
        }

        public override string ToString()
        {
            return "LOAD " + Name.ToPrintable();
        }
    }
}
