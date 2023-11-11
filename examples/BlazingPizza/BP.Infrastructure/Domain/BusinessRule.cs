namespace BP.Infrastructure.Domain
{
    public class BusinessRule
    {
        private string _ruleDescription;

        public BusinessRule(string ruleDescription)
        {
            _ruleDescription = ruleDescription;
        }

        public string RuleDescription
        {
            get
            {
                return _ruleDescription;
            }
        }
    }
}
