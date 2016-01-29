using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Domain
{
    public static class PropertyNavigatorExtensions
    {
        public static PropertyNavigator<T> Include<T>(this PropertyNavigator<T> entity, Expression<Func<T, object>> path)
            where T : class
        {
            entity.AddPath(path);
            return entity;
        }
    }
}
