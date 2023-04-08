using Backender.CodeEditor.CSharp.Objects;
using Attribute = Backender.CodeEditor.CSharp.Objects.Attribute;
using Enum = Backender.CodeEditor.CSharp.Objects.Enum;

namespace Backender.CodeEditor.CSharp
{
	public static class CodeBuilder
	{
		public static Project AddProject(this Solution solution, string projectName, string defaultNameSpace = "")
		{
			var project = new Project()
			{
				Name = projectName,
				SolutionName = solution.Name,
				DefaultNameSpace = defaultNameSpace
			};
			project = solution.AddProject(project);
			return project;
		}
		public static Project AddProject(this Solution solution, Project project)
		{
			if (project.DefaultNameSpace == "")
			{
				project.DefaultNameSpace = project.Name;
			}
			solution.Projects.Add(project);
			return project;
		}
		public static Project AddProjectReference(this Project project, Project projectReference)
		{
			project.ProjectReference.Add(projectReference);
			return project;
		}
		public static Project GetProjectByName(this Solution solution, string projectName)
		{

			var project = solution.Projects.FirstOrDefault(p => p.Name == projectName);
			return project;
		}

		public static Class GetClassByName(this Project project, string className)
		{

			var _class = (Class)project.CsFiles.FirstOrDefault(p => p.Name == className && p is Class);
			return _class;
		}
		public static bool IsClassExist(this Project project, string className)
		{
			return project.CsFiles.Any(p => p.Name == className && p is Class);
		}
		public static Class AddClass(this Project project, Class _class)
		{
			project.CsFiles.Add(_class);
			return _class;
		}
		public static Class AddClass(this Project project, string className, AccessModifier accessModifier = AccessModifier.Public, string AppendNameSpace = "", List<Attribute> attributes = null, bool isStatic = false, string baseClassName = "", List<string> Options = null)
		{
			var _class = new Class()
			{
				Name = className,
				AccessModifier = accessModifier,
				IsStatic = isStatic,
				NameSpace = project.DefaultNameSpace,
				ProjectName = project.Name,
				BaseClassName = baseClassName,
				Options = Options
			};
			if (!string.IsNullOrEmpty(AppendNameSpace))
			{
				_class.NameSpace += "." + AppendNameSpace;
			}
			if (attributes != null)
			{
				_class.Attributes = attributes;
			}
			_class = project.AddClass(_class);
			return _class;
		}

		public static Interface ToInterface(this Class _class)
		{
			var methods = _class.InnerItems.Where(p => p is Method).ToList();
			var _interface = new Interface()
			{
				AccessModifier = _class.AccessModifier,
				InnerItems = methods,
				Name = "I" + _class.Name,
				NameSpace = _class.NameSpace,
				ProjectName = _class.ProjectName,
				UsingNameSpaces = _class.UsingNameSpaces,
			};
			return _interface;
		}
		public static Interface AddInterface(this Project project, Interface _interface)
		{
			project.CsFiles.Add(_interface);
			return _interface;
		}

		public static Enum AddEnum(this Project project, Enum _enum)
		{
			project.CsFiles.Add(_enum);
			return _enum;
		}
		public static Enum AddEnum(this Project project, string EnumName, List<EnumValue> EnumValues, string AppendNameSpace)
		{
			var _enum = new Enum()
			{
				Name = EnumName,
				AccessModifier = AccessModifier.Public,
				NameSpace = project.DefaultNameSpace,
				ProjectName = project.Name,
				EnumValues = EnumValues

			};
			if (!string.IsNullOrEmpty(AppendNameSpace))
			{
				_enum.NameSpace += "." + AppendNameSpace;
			}
			return project.AddEnum(_enum);
		}
		public static Property AddProperty(this Class _class, Property property)
		{
			_class.InnerItems.Add(property);
			return property;
		}
		public static Property AddProperty(this Class _class,
			string propertyDataType,
			string propertyName,
			AccessModifier accessModifier = AccessModifier.Public,
			bool isVirtual = false,
			string getInnerCode = "",
			string setInnerCode = "")
		{
			var property = new Property()
			{
				AccessModifier = accessModifier,
				Name = propertyName,
				DataType = propertyDataType,
				ClassName = _class.Name,
				IsVirtual = isVirtual,
				GetInnerCode= getInnerCode,
				SetInnerCode= setInnerCode
			};
			property = _class.AddProperty(property);
			return property;
		}

		public static Method AddMethod(this Class _class, Method method)
		{
			_class.InnerItems.Add(method);
			return method;
		}
		public static Method AddMethod(this Class _class, string methodDataType, string methodName, string innerCode, MethodParameter parameter = null, AccessModifier accessModifier = AccessModifier.Public)
		{
			var parameters = new List<MethodParameter>();
			parameters.Add(parameter);
			var method = _class.AddMethod(methodDataType, methodName, innerCode, parameters, accessModifier);
			return method;
		}
		public static Method AddMethod(this Class _class, string methodDataType, string methodName, string innerCode, List<MethodParameter> parameters, AccessModifier accessModifier = AccessModifier.Public)
		{
			var method = new Method()
			{
				AccessModifier = accessModifier,
				Name = methodName,
				DataType = methodDataType,
				ClassName = _class.Name,
				InnerCode = innerCode,
			};
			if (parameters != null)
			{
				method.Parameters = parameters;
			}
			_class.InnerItems.Add(method);
			return method;
		}

		public static Field AddField(this Class _class, Field field)
		{
			_class.InnerItems.Add(field);
			return field;
		}
		public static Field AddField(this Class _class, string fieldDataType, string fieldName, bool AutoImplimented = true, AccessModifier accessModifier = AccessModifier.Public, string defaultValue = "")
		{
			var field = new Field()
			{
				AccessModifier = accessModifier,
				Name = fieldName,
				DataType = fieldDataType,
				ClassName = _class.Name,
				DefaultValue = defaultValue,
				AllowAutoImplement = AutoImplimented
			};
			field = _class.AddField(field);
			return field;
		}

		public static Constructor AddConstructor(this Class _class, Constructor constructor)
		{
			_class.InnerItems.Add(constructor);
			return constructor;
		}
		public static Constructor AddConstructor(this Class _class, string innerCode, List<MethodParameter> parameters, AccessModifier accessModifier = AccessModifier.Public, string defaultValue = "")
		{
			var constructor = new Constructor()
			{
				AccessModifier = accessModifier,
				ClassName = _class.Name,
				Parameters = parameters,
				InnerCode = innerCode
			};
			constructor = _class.AddConstructor(constructor);
			return constructor;
		}
		public static Constructor GetConstructorByClassName(this Class _class, string className)
		{

			var _constructor = _class.InnerItems.FirstOrDefault(p => p.ClassName == className && p is Constructor);
			return (Constructor)_constructor;
		}

		public static Property AddAttributeToProperty(this Property property, Attribute attribute)
		{
			property.Attributes.Add(attribute);
			return property;
		}
		public static Property AddAttributeToProperty(this Property property, string attributeName, string attributeValue)
		{
			var attribute = new Attribute()
			{
				Name = attributeName,
				Value = attributeValue,
				AccessModifier = AccessModifier.None
			};
			return property.AddAttributeToProperty(attribute);
		}
		public static Property AddAttributeToProperty(this Property property, string attributeName, List<AttributeParameter> parameters = null)
		{
			var attribute = new Attribute()
			{
				Name = attributeName,
				AccessModifier = AccessModifier.None
			};
			if (parameters != null)
			{
				attribute.Parameters = parameters;
			}
			return property.AddAttributeToProperty(attribute);
		}
		public static TCodeObject AddRequiredNameSpaces<TCodeObject>(this TCodeObject codeObject, params string[] requiredNameSpaces) where TCodeObject : CodeObject
		{
			codeObject.RequiredNameSpaces.AddRange(requiredNameSpaces);
			return codeObject;
		}


	}
}
