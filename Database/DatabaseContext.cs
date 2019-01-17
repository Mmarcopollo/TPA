using Database.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Project;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework") { }

        public virtual DbSet<AssemblyMetadataDTO> AssemblyMetadata { get; set; }
        public virtual DbSet<NamespaceMetadataDTO> NamespaceMetadata { get; set; }
        public virtual DbSet<TypeMetadataDTO> TypeMetadata { get; set; }
        public virtual DbSet<FieldMetadataDTO> FieldMetadata { get; set; }
        public virtual DbSet<PropertyMetadataDTO> PropertyMetadata { get; set; }
        public virtual DbSet<MethodMetadataDTO> MethodMetadata { get; set; }
        public virtual DbSet<ParameterMetadataDTO> ParameterMetadata { get; set; }
        public virtual DbSet<DBLog> Log { get; set; }
    }
}
