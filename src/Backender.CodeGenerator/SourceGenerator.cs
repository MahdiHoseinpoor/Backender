using Backender.CodeEditor.CSharp.Objects;
using Backender.CodeGenerator.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Enum = Backender.CodeEditor.CSharp.Objects.Enum;

namespace Backender.CodeGenerator
{
	public class SourceGenerator
	{
		public SourceGenerator()
		{
		}

		public SourceFile ClassToSource(Class _class)
		{
			var SourceFile = new SourceFile();
			var source = BaseSources.ClassSource;
			if (string.IsNullOrEmpty(_class.BaseClassName))
			{
				source = source.Replace("$ClassName$", _class.Name);
			}
			else
			{
				source = source.Replace("$ClassName$", _class.Name + ":" + _class.BaseClassName);
			}
			source = SetNameSpace(source, _class.NameSpace);
			source = SetAccessModifier(source, _class.AccessModifier);
			var innerObjects = new List<string>();
			var MethodObjects = new List<string>();
			var FieldObjects = new List<string>();
			var PropertyObjects = new List<string>();
			var ConstructorObjects = new List<string>();
			foreach (var classitem in _class.InnerItems)
			{
				_class.UsingNameSpaces.AddRange(classitem.RequiredNameSpaces);

				if (classitem is Method)
				{
					MethodObjects.Add(MethodToSource((Method)classitem));
				}
				else if (classitem is Property)
				{
					Property property = (Property)classitem;
					foreach (var attribute in property.Attributes)
					{
						_class.UsingNameSpaces.AddRange(attribute.RequiredNameSpaces);
					}
					PropertyObjects.Add(PropertyToSource(property));
				}
				else if (classitem is Field)
				{
					FieldObjects.Add(FieldToSource((Field)classitem));
				}
				else if (classitem is Constructor)
				{
					ConstructorObjects.Add(ConstructorToSource((Constructor)classitem));
				}
				//switch (classitem.ClassItemType)
				//{
				//    case ClassItemType.Method:
				//        innerObjects.Add(MethodToSource())
				//        break;
				//    case ClassItemType.Property:
				//        break;
				//    case ClassItemType.Field:
				//        break;
				//    default:
				//        break;
				//}
			}
			innerObjects.AddRange(FieldObjects);
			innerObjects.AddRange(ConstructorObjects);
			innerObjects.AddRange(PropertyObjects);
			innerObjects.AddRange(MethodObjects);
			_class.UsingNameSpaces = _class.UsingNameSpaces.Distinct().ToList();
			source = AddUsings(source, _class.UsingNameSpaces);
			source = source.Replace("$InnerObjects$", string.Join("\n", innerObjects));

			SourceFile.Name = _class.Name;
			SourceFile.SourceCode = source;
			SourceFile.ProjectName = _class.ProjectName;
			return SourceFile;
		}
		public SourceFile InterfaceToSource(Interface _interface)
		{
			var SourceFile = new SourceFile();

			var source = BaseSources.InterfaceSource;
			source = SetAccessModifier(source, _interface.AccessModifier);
			source = source.Replace("$InterfaceName$", _interface.Name);
			source = source.Replace("$NameSpace$", _interface.NameSpace);

			source = AddUsings(source, _interface.UsingNameSpaces);

			var innerObjects = new List<string>();
			var MethodObjects = new List<string>();
			foreach (var _InterfaceItem in _interface.InnerItems)
			{
				if (_InterfaceItem is Method)
				{
					MethodObjects.Add(MethodToSource((Method)_InterfaceItem, true));
				}
			}
			innerObjects.AddRange(MethodObjects);
			source = source.Replace("$InnerObjects$", string.Join("\n", innerObjects));

			SourceFile.Name = _interface.Name;

			SourceFile.SourceCode = source;
			SourceFile.ProjectName = _interface.ProjectName;
			return SourceFile;
		}
		public SourceFile EnumToSource(Enum _enum)
		{
			var SourceFile = new SourceFile();
			var source = BaseSources.EnumSource;
			source = SetAccessModifier(source, _enum.AccessModifier);
			source = source.Replace("$EnumName$", _enum.Name);
			source = source.Replace("$NameSpace$", _enum.NameSpace);
			var EnumValues = new List<string>();
			foreach (var enumValue in _enum.EnumValues)
			{
				
				var _enumValue = enumValue.Name + " = " + enumValue.Value;
				EnumValues.Add(_enumValue);
			}
			
			source = source.Replace("$EnumValues$", string.Join(",\n", EnumValues));

			SourceFile.Name = _enum.Name;

			SourceFile.SourceCode = source;
			SourceFile.ProjectName = _enum.ProjectName;
			return SourceFile;
		}
		public static string AddUsings(string source, List<string> usings)
		{
			var Usings = new List<string>();
			foreach (var Namespace in usings)
			{
				Usings.Add($"using {Namespace};");
			}
			source = source.Replace("$Usings$", string.Join("\n", Usings));
			return source;
		}
		public static string SetNameSpace(string source, string nameSpace)
		{
			return source.Replace("$NameSpace$", nameSpace);
		}
		public static string SetAccessModifier(string source, AccessModifier accessModifier)
		{
			var _accessModifier = "";
			switch (accessModifier)
			{
				case AccessModifier.Public:
					_accessModifier = "public";
					break;
				case AccessModifier.Protected:
					_accessModifier = "protected";
					break;
				case AccessModifier.Private:
					_accessModifier = "private";
					break;
				case AccessModifier.Internal:
					_accessModifier = "internal";
					break;
				case AccessModifier.None:
					break;
				default:
					break;
			}
			source = source.Replace("$AccessModifier$", _accessModifier);
			return source;
		}
		public string MethodToSource(Method method, bool IsForInterface = false)
		{
			var source = BaseSources.MethodSource;
			source = SetAccessModifier(source, method.AccessModifier);
			source = source.Replace("$DataType$", method.DataType);
			source = source.Replace("$Name$", method.Name);
			if (method.Parameters != null)
			{
				if (method.Parameters.Any())
				{
					var Parameters = new List<string>();
					foreach (var Parameter in method.Parameters)
					{
						if (Parameter != null)
						{
							var finalParameter = Parameter.DataType + " " + Parameter.Name;
							if (!string.IsNullOrEmpty(Parameter.DefaultValue))
							{
								finalParameter += " = " + Parameter.DefaultValue;
							}
							Parameters.Add(finalParameter);
						}
					}
					source = source.Replace("$Parameters$", string.Join(',', Parameters));
				}
			}
			if (IsForInterface)
			{
				source += ";";
			}
			else
			{
				source += $@"
{{
    {method.InnerCode}
}}
";
			}
			return source;
		}
		public string PropertyToSource(Property property)
		{
			var attributesInSource = new List<string>();

			foreach (var attribute in property.Attributes)
			{
				var attributeSource = $"[{attribute.Name}";
				var Parameters = new List<string>();
				if (attribute.Parameters.Any())
				{
					foreach (var atributeParameter in attribute.Parameters)
					{
						var parameter = atributeParameter.Name + " = " + atributeParameter.DefaultValue;
						Parameters.Add(parameter);
					}
					attributeSource +="("+ string.Join(',',Parameters)+")";
				}
			    else if (!string.IsNullOrEmpty(attribute.Value))
				{
					attributeSource +="("+ attribute.Value+ ")";
				}
				attributeSource += "]";
				attributesInSource.Add(attributeSource);
			}

			var source = string.Join('\n', attributesInSource)+ "\n" +BaseSources.PropertySource;
			source = SetAccessModifier(source, property.AccessModifier);
			var Modifiers = "";
			if (property.IsVirtual)
			{
				Modifiers = "virtual";
			}
			source = source.Replace("$Modifiers$", Modifiers);
			source = source.Replace("$DataType$", property.DataType);
			source = source.Replace("$Name$", property.Name);

			return source;
		}
		public string ConstructorToSource(Constructor constructor)
		{
			var source = BaseSources.ConstructorSource;
			source = SetAccessModifier(source, constructor.AccessModifier);
			source = source.Replace("$ClassName$", constructor.ClassName);
			if (constructor.Parameters != null)
			{
				if (constructor.Parameters.Any())
				{
					var Parameters = new List<string>();
					foreach (var Parameter in constructor.Parameters)
					{
						if (Parameter != null)
						{
							var finalParameter = Parameter.DataType + " " + Parameter.Name;
							if (!string.IsNullOrEmpty(Parameter.DefaultValue))
							{
								finalParameter += " = " + Parameter.DefaultValue;
							}
							Parameters.Add(finalParameter);
						}
					}
					source = source.Replace("$Parameters$", string.Join(',', Parameters));
				}
			}
			source = source.Replace("$InnerCode$", constructor.InnerCode);
			return source;
		}
		public string FieldToSource(Field field)
		{
			var source = BaseSources.FieldSource;
			source = SetAccessModifier(source, field.AccessModifier);
			var Modifiers = "";
			//if (field.)
			//{
			//    Modifiers = "virtual";
			//}
			source = source.Replace("$Modifiers$", Modifiers);
			source = source.Replace("$DataType$", field.DataType);
			source = source.Replace("$Name$", field.Name);
			if (!string.IsNullOrEmpty(field.DefaultValue))
			{
				source = source.Replace("$DefaultValue$", field.DefaultValue);
			}
			else
			{
				source = source.Replace("$DefaultValue$", "");
			}
			return source;
		}
		public string GetFilePath(Project proj, string nameSpace)
		{
			var Folders = new List<string>();
			if (nameSpace != proj.DefaultNameSpace)
			{
				Folders.AddRange(nameSpace.Replace(proj.DefaultNameSpace + ".", "").Split('.'));
			}
			return Path.Combine(proj.Name, string.Join('\\', Folders));
		}
	}
}
