using BasicData;
using Database.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Export(typeof(ISerializer))]
    public class DatabaseSerializer : ISerializer
    {
         public BaseAssemblyMetadata Read(string path)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.NamespaceMetadata
                    .Include(x => x.Types)
                    .Load();
                context.TypeMetadata
                    .Include(x => x.BaseType)
                    .Include(x => x.GenericArguments)
                    //.Include(x => x.Attributes)
                    .Include(x => x.ImplementedInterfaces)
                    .Include(x => x.NestedTypes)
                    .Include(x => x.Fields)
                    .Include(x => x.Properties)
                    .Include(x => x.DeclaringType)
                    .Include(x => x.Methods)
                    .Include(x => x.Constructors)
                    .Load();
                context.FieldMetadata
                    .Include(x => x.FieldType)
                    .Include(x => x.Attributes)
                    .Load();
                context.ParameterMetadata
                    .Include(x => x.UsedTypeMetadata)
                    .Load();
                context.MethodMetadata
                    .Include(x => x.GenericArguments)
                    .Include(x => x.ReturnType)
                    .Include(x => x.Parameters)
                    .Load();
                context.PropertyMetadata
                    .Include(x => x.UsedTypeMetadata)
                    .Load();


                AssemblyMetadataDTO assemblyMetadata = context.AssemblyMetadata.Include(x => x.Namespaces).ToList().FirstOrDefault();
                if (assemblyMetadata == null) throw new Exception("Database is empty");
                return assemblyMetadata;
            }
        }

        public void Write(BaseAssemblyMetadata obj, string path)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ParameterMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM PropertyMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM MethodMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM TypeMetadata ");
                context.Database.ExecuteSqlCommand("DELETE FROM NamespaceMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM AssemblyMetadata WHERE ID != -1");
                context.SaveChanges();
                AssemblyMetadataDTO assemblyMetadata = (AssemblyMetadataDTO)obj;
                context.AssemblyMetadata.Add(assemblyMetadata);
                context.SaveChanges();
            }
        }
    }
}
