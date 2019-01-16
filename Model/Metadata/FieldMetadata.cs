using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BasicData;
using Serialization;
using Serialization.DTO;

namespace Model.Metadata
{
    public class FieldMetadata : BaseFieldMetadata
    {
        #region data

        public override Guid Guid
        {
            get => base.Guid;
            set => base.Guid = value;
        }

        public override string FieldName
        {
            get => base.FieldName;
            set => base.FieldName = value;
        }

        public override bool IsReadOnly
        {
            get => base.IsReadOnly;
            set => base.IsReadOnly = value;
        }

        public new TypeMetadata FieldType
        {
            get => (TypeMetadata)base.FieldType;
            set => base.FieldType = value;
        }

        public override Tuple<AccessLevel, StaticEnum> Modifiers
        {
            get => base.Modifiers;
            set => base.Modifiers = value;
        }

        public new IEnumerable<TypeMetadata> Attributes
        {
            get => (IEnumerable<TypeMetadata>)base.Attributes;
            set => base.Attributes = value;
        }

        #endregion

        #region constructors

        public FieldMetadata(FieldInfo field)
        {
            Guid = Guid.NewGuid();
            FieldName = field.Name;
            FieldType = EmitFieldType(field.FieldType);
            IsReadOnly = field.IsInitOnly;
            Modifiers = EmitModifiers(field);
            Attributes = field.GetCustomAttributes(false).Select(x => TypeMetadata.EmitReference(x.GetType()));
        }

        #endregion

        #region api

        private static TypeMetadata EmitFieldType(Type type)
        {
            if (type == null)
                return null;
            return TypeMetadata.EmitReference(type);
        }

        private static Tuple<AccessLevel, StaticEnum> EmitModifiers(FieldInfo field)
        {
            AccessLevel _access = AccessLevel.IsPrivate;
            StaticEnum _static = StaticEnum.NotStatic;

            if (field.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (field.IsFamily)
                _access = AccessLevel.IsProtected;
            else if (field.IsFamilyAndAssembly)
                _access = AccessLevel.IsProtectedInternal;
            else if (field.IsAssembly)
                _access = AccessLevel.IsInternal;

            if (field.IsStatic)
                _static = StaticEnum.Static;

            return new Tuple<AccessLevel, StaticEnum>(_access, _static);
        }

        #endregion

        #region DTO

        public FieldMetadataDTO Convert()
        {
            FieldMetadataDTO fieldDTO = new FieldMetadataDTO();

            //GUID
            fieldDTO.Guid = Guid;

            // Name
            fieldDTO.FieldName = FieldName;

            //Read Only
            fieldDTO.IsReadOnly = IsReadOnly;

            //FieldType
            if (FieldType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(FieldType.TypeName))
                {
                    fieldDTO.FieldType = TypeMetadataDTO.DTOTypeDictionary[FieldType.TypeName];
                }
                else
                {
                    fieldDTO.FieldType = FieldType.ConvertToDTO();
                }
            }

            //Field Modifiers
            fieldDTO.Modifiers = Modifiers;

            //Field Attributes
            if (Attributes != null)
            {
                List<TypeMetadataDTO> tempAttributes = new List<TypeMetadataDTO>();
                foreach (TypeMetadata metadata in Attributes)
                {
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(metadata.TypeName))
                    {
                        tempAttributes.Add(TypeMetadataDTO.DTOTypeDictionary[metadata.TypeName]);
                    }
                    else
                    {
                        tempAttributes.Add(metadata.ConvertToDTO());
                    }
                }

                fieldDTO.Attributes = tempAttributes;
            }



            return fieldDTO;
        }

        public FieldMetadata(BaseFieldMetadata baseFields)
        {
            //GUID
            Guid = baseFields.Guid;

            // Name
            FieldName = baseFields.FieldName;

            //Read Only
            IsReadOnly = baseFields.IsReadOnly;

            //FieldType
            if (baseFields.FieldType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(baseFields.FieldType.TypeName))
                {
                    FieldType = TypeMetadata.TypeDictionary[baseFields.FieldType.TypeName];
                }
                else
                {
                    FieldType = new TypeMetadata((TypeMetadataDTO)baseFields.FieldType);
                }
            }

            //Field Modifiers
            Modifiers = baseFields.Modifiers;

            //Field Attributes
            if (baseFields.Attributes != null)
            {
                List<TypeMetadata> tempAttributes = new List<TypeMetadata>();
                foreach (TypeMetadataDTO metadata in baseFields.Attributes)
                {
                    if (TypeMetadata.TypeDictionary.ContainsKey(metadata.TypeName))
                    {
                        tempAttributes.Add(TypeMetadata.TypeDictionary[metadata.TypeName]);
                    }
                    else
                    {
                        tempAttributes.Add(new TypeMetadata(metadata));
                    }
                }

                Attributes = tempAttributes;
            }
        }

        #endregion

        #region overrides

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion overrides
    }
}


