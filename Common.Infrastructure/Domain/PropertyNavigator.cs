using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Domain
{
    public class PropertyNavigator<T>
            where T : class
    {
        // Fields
        private List<Expression<Func<T, object>>> _paths;

        // Properties
        public Expression<Func<T, object>>[] Properties
        {
            get { return _paths.ToArray(); }
        }

        // Methods
        public void AddPath(Expression<Func<T, object>> path)
        {
            if (_paths == null)
            {
                _paths = new List<Expression<Func<T, object>>>();
            }
            _paths.Add(path);
        }
    }
}
