using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB
{
    partial class QuestionnaireDBContext
    {
        
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void UpdateEntitiesState(object entity)
        {
            var temp = new List<object>();
            UpdateEntitiesState(entity, ref temp);
        }

        private void UpdateEntitiesState(object entity, ref List<object> visited)
        {
            if (entity == null) return;

            // Avoid circular references
            if (visited == null) visited = new List<object>();
            if (visited.Contains(entity)) return;
            visited.Add(entity);
            // -----------------------------------------------------


            // If it doesn't have an ID it's not a db entity so we skip this
            if (entity.GetType().GetProperties().All(p => p.Name.ToLower() != "id")) return;

            // Fetching all the entity properties. If it's an ID we check wether is a 0 (new entity) or another existing ID.
            // If it is an object we recursively call this method. If it is an array, we call this method for every item in it.
            foreach (var propertyInfo in entity.GetType().GetProperties())
            {
                // It's an aray, but not a string
                if (propertyInfo.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null &&
                    !(propertyInfo.GetValue(entity) is String))
                {
                    IEnumerable<object> objects = (IEnumerable<object>)propertyInfo.GetValue(entity);
                    if (objects != null)
                    {
                        foreach (var o in objects)
                        {
                            UpdateEntitiesState(o, ref visited);
                        }
                    }
                }
                else
                {
                    UpdateEntitiesState(propertyInfo.GetValue(entity), ref visited);
                }

            }
            var idProp = entity.GetType().GetProperties().Single(x => x.Name.ToLower() == "id");
            if (idProp != null)
            {
                int id = (int)idProp.GetValue(entity);
                if (id != 0)
                {
                    var deleted = entity.GetType().GetProperties().SingleOrDefault(x => x.Name.ToLower() == "deleted");
                    if (deleted != null)
                    {
                        if ((bool) deleted.GetValue(entity))
                        {
                            this.Entry(entity).State = EntityState.Deleted;
                            
                        }
                    }
                    else
                    {
                        this.Entry(entity).State = EntityState.Modified;
                    }
                }
                else
                {
                    this.Entry(entity).State = EntityState.Added;
                }
            }
        }
    }
}
