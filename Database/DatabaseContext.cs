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

        public virtual DbSet<AssemblyMetadataDatabaseDTO> AssemblyMetadata { get; set; }
        public virtual DbSet<NamespaceMetadataDatabaseDTO> NamespaceMetadata { get; set; }
        public virtual DbSet<TypeMetadataDatabaseDTO> TypeMetadata { get; set; }
        public virtual DbSet<FieldMetadataDatabaseDTO> FieldMetadata { get; set; }
        public virtual DbSet<PropertyMetadataDatabaseDTO> PropertyMetadata { get; set; }
        public virtual DbSet<MethodMetadataDatabaseDTO> MethodMetadata { get; set; }
        public virtual DbSet<ParameterMetadataDatabaseDTO> ParameterMetadata { get; set; }
    }
}
