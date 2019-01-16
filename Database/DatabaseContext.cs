using Database.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LocalDatabase.mdf;Integrated Security=True") { }

        public virtual DbSet<AssemblyMetadataDTO> AssemblyMetadata { get; set; }
        public virtual DbSet<NamespaceMetadataDTO> NamespaceMetadata { get; set; }
        public virtual DbSet<TypeMetadataDTO> TypeMetadata { get; set; }
        public virtual DbSet<FieldMetadataDTO> FieldMetadata { get; set; }
        public virtual DbSet<PropertyMetadataDTO> PropertyMetadata { get; set; }
        public virtual DbSet<MethodMetadataDTO> MethodMetadata { get; set; }
        public virtual DbSet<ParameterMetadataDTO> ParameterMetadata { get; set; }
    }
}
