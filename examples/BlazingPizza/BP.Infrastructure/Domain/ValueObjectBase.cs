﻿using System.Collections.Generic;
using System.Text;

namespace BP.Infrastructure.Domain
{
    public abstract class ValueObjectBase
    {
        private List<BusinessRule> _brokenRules = new();

        public ValueObjectBase()
        {
        }

        protected abstract void Validate();

        public void ThrowExceptionIfInvalid()
        {
            _brokenRules.Clear();
            Validate();
            if (_brokenRules.Count > 0)
            {
                StringBuilder issues = new();
                foreach (BusinessRule businessRule in _brokenRules)
                {
                    issues.AppendLine(businessRule.RuleDescription);
                }

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }
    }
}
