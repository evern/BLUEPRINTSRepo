using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class OptionalEntitiesParameter<TEntity, TSecondEntity>
        where TEntity : class
        where TSecondEntity : class
    {

        TEntity entity;
        TSecondEntity secondEntity;
        public OptionalEntitiesParameter(TEntity entity, TSecondEntity secondEntity)
        {
            this.entity = entity;
            this.secondEntity = secondEntity;
        }

        public TEntity GetFirstEntity()
        {
            return this.entity;
        }

        public TSecondEntity GetSecondEntity()
        {
            return this.secondEntity;
        }

        public override string ToString()
        {
            if (entity != null)
                return entity.GetType().GetProperty("GUID").GetValue(entity).ToString();
            else if (secondEntity != null)
                return secondEntity.GetType().GetProperty("GUID").GetValue(entity).ToString();
            else
                return string.Empty;
        }
    }
}
