using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class TypeMetadataDTO : BaseTypeMetadata
    {
        public static Dictionary<string, TypeMetadataDTO> DTOTypeDictionary = new Dictionary<string, TypeMetadataDTO>();

        [DataMember]
        public override string TypeName { get => base.TypeName; set => base.TypeName = value; }
        [DataMember]
        public override string NamespaceName { get => base.NamespaceName; set => base.NamespaceName = value; }
        [DataMember]
        public override BaseTypeMetadata BaseType { get => base.BaseType; set => base.BaseType = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> GenericArguments { get => base.GenericArguments; set => base.GenericArguments = value; }
        [DataMember]
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        [DataMember]
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        [DataMember]
        public override SealedEnum SealedEnum { get => base.SealedEnum; set => base.SealedEnum = value; }
        [DataMember]
        public override TypeKind TypeKind { get => base.TypeKind; set => base.TypeKind = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> ImplementedInterfaces { get => base.ImplementedInterfaces; set => base.ImplementedInterfaces = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> NestedTypes { get => base.NestedTypes; set => base.NestedTypes = value; }
        [DataMember]
        public override IEnumerable<BasePropertyMetadata> Properties { get => base.Properties; set => base.Properties = value; }
        [DataMember]
        public override BaseTypeMetadata DeclaringType { get => base.DeclaringType; set => base.DeclaringType = value; }
        [DataMember]
        public override IEnumerable<BaseMethodMetadata> Methods { get => base.Methods; set => base.Methods = value; }
        [DataMember]
        public override IEnumerable<BaseMethodMetadata> Constructors { get => base.Constructors; set => base.Constructors = value; }
        [DataMember]
        public override IEnumerable<BaseFieldMetadata> Fields { get => base.Fields; set => base.Fields = value; }

        public TypeMetadataDTO(BaseTypeMetadata typeMetadataDTO)
        {
            TypeName = typeMetadataDTO.TypeName;
            NamespaceName = typeMetadataDTO.NamespaceName;

            if (typeMetadataDTO.BaseType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(typeMetadataDTO.BaseType.TypeName)) BaseType = TypeMetadataDTO.DTOTypeDictionary[typeMetadataDTO.BaseType.TypeName];
                else BaseType = new TypeMetadataDTO(typeMetadataDTO.BaseType);
            }

            if (typeMetadataDTO.GenericArguments != null)
            {
                List<TypeMetadataDTO> arguments = new List<TypeMetadataDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.GenericArguments)
                {
                    TypeMetadataDTO metadata;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDTO.DTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDTO(DTO);
                    arguments.Add(metadata);
                }
                GenericArguments = arguments;
            }

            AccessLevel = typeMetadataDTO.AccessLevel;
            AbstractEnum = typeMetadataDTO.AbstractEnum;
            SealedEnum = typeMetadataDTO.SealedEnum;
            TypeKind = typeMetadataDTO.TypeKind;


            if (typeMetadataDTO.ImplementedInterfaces != null)
            {
                List<TypeMetadataDTO> interfaces = new List<TypeMetadataDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.ImplementedInterfaces)
                {
                    TypeMetadataDTO metadata;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDTO.DTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDTO(DTO);
                    interfaces.Add(metadata);
                }
                ImplementedInterfaces = interfaces;
            }

            if (typeMetadataDTO.NestedTypes != null)
            {
                List<TypeMetadataDTO> nested = new List<TypeMetadataDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.NestedTypes)
                {
                    TypeMetadataDTO metadata;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDTO.DTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDTO(DTO);
                    nested.Add(metadata);
                }
                NestedTypes = nested;
            }

            if (typeMetadataDTO.Properties != null)
            {
                List<PropertyMetadataDTO> properties = new List<PropertyMetadataDTO>();
                foreach (BasePropertyMetadata DTO in typeMetadataDTO.Properties)
                {
                    PropertyMetadataDTO propertyMetadata = new PropertyMetadataDTO(DTO);
                    properties.Add(propertyMetadata);
                }
                Properties = properties;
            }

            if (typeMetadataDTO.DeclaringType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(typeMetadataDTO.DeclaringType.TypeName)) DeclaringType = TypeMetadataDTO.DTOTypeDictionary[typeMetadataDTO.DeclaringType.TypeName];
                else DeclaringType = new TypeMetadataDTO(typeMetadataDTO.DeclaringType);
            }

            if (typeMetadataDTO.Methods != null)
            {
                List<MethodMetadataDTO> methods = new List<MethodMetadataDTO>();
                foreach (BaseMethodMetadata DTO in typeMetadataDTO.Methods)
                {
                    MethodMetadataDTO methodMetadata = new MethodMetadataDTO(DTO);
                    methods.Add(methodMetadata);
                }
                Methods = methods;
            }

            if (typeMetadataDTO.Constructors != null)
            {
                List<MethodMetadataDTO> constructors = new List<MethodMetadataDTO>();
                foreach (BaseMethodMetadata DTO in typeMetadataDTO.Constructors)
                {
                    MethodMetadataDTO methodMetadata = new MethodMetadataDTO(DTO);
                    constructors.Add(methodMetadata);
                }
                Constructors = constructors;
            }

            if (typeMetadataDTO.Fields != null)
            {
                List<DTO.FieldMetadataDTO> fields = new List<DTO.FieldMetadataDTO>();
                foreach (BaseFieldMetadata DTO in typeMetadataDTO.Fields)
                {
                    DTO.FieldMetadataDTO fieldMetadata = new DTO.FieldMetadataDTO(DTO);
                    fields.Add(fieldMetadata);
                }
                Fields = fields;
            }

            if (!DTOTypeDictionary.ContainsKey(this.TypeName))
            {
                DTOTypeDictionary.Add(TypeName, this);
            }
        }
    }
}
