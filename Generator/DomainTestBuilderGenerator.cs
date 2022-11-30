using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator;

[Generator]
public class DomainTestBuilderGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {

    }

    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;
        var domainInterfaces = compilation.GetTypeByMetadataName("Domain.IEntity");
        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            var immutableHashSet = syntaxTree.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .Select(x => semanticModel.GetDeclaredSymbol(x))
                .OfType<ITypeSymbol>()
                .Where(x => x.Interfaces.Contains(domainInterfaces))
                .ToImmutableHashSet();

            foreach (var typeSymbol in immutableHashSet)
            {
                var source = GenerateBuilder(typeSymbol);
                context.AddSource($"{typeSymbol.Name}.Test.cs", source);
            }
        }
    }

    private string GenerateBuilder(ITypeSymbol typeSymbol)
    {
        return $@"namespace {typeSymbol.ContainingNamespace}.Tests
{{
  public class {typeSymbol.Name}Builder
  {{
    {GenerateBackingFieldsAndIdProperties(typeSymbol)}
    {GenerateBuilderMethods(typeSymbol)}
    {GenerateBuildMethod(typeSymbol)}
  }}
}}";
    }

    private string GenerateBuildMethod(ITypeSymbol typeSymbol)
    {
        var createMethod = GetCreteMethod(typeSymbol);
        var createParameters = createMethod.Parameters;
        var sb = new StringBuilder();
        var fieldsAndProperties = GetFieldsAndPropertiesThatInitializedWithCreateMethod(typeSymbol);
        var arguments = new List<ISymbol>();

        foreach (var parameter in createParameters)
        {
            arguments.Add(
                fieldsAndProperties.FirstOrDefault(fp => fp.Name.ToLower().Contains(parameter.Name.ToLower())));
        }


        sb.Append($@"
    public {typeSymbol.Name} Build()
    {{   
        return {typeSymbol.Name}.Create({string.Join(", ", arguments.Select(a => a.Name))});
    }}
");
        return sb.ToString();
    }

    private string GenerateBuilderMethods(ITypeSymbol typeSymbol)
    {
        var sb = new StringBuilder();

        var suffix = "_";
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        var fields = GetFieldsAndPropertiesThatInitializedWithCreateMethod(typeSymbol);

        foreach (var fieldSymbol in fields)
        {
            var fieldBuilderMethodName = "Set" + textInfo.ToTitleCase(fieldSymbol.Name[1..]);
            var PropertyBuilderMethodName = "Set" + textInfo.ToTitleCase(fieldSymbol.Name);
            if (typeSymbol.BaseType != null)
            {
                if ((fieldSymbol as IFieldSymbol) != null)
                {
                    sb.AppendLine($@"
    public {typeSymbol.Name}Builder {fieldBuilderMethodName}({(fieldSymbol as IFieldSymbol).Type} {fieldSymbol.Name[1..]})
    {{
        {fieldSymbol.Name} = {fieldSymbol.Name[1..]};
        return this;
    }}"
                    );
                }
                else if ((fieldSymbol as IPropertySymbol) != null)
                {
                    sb.AppendLine($@"
    public {typeSymbol.Name}Builder {PropertyBuilderMethodName}({(fieldSymbol as IPropertySymbol).Type} {fieldSymbol.Name})
    {{
        {fieldSymbol.Name} = {fieldSymbol.Name};
        return this;
    }}"
                    );
                }

            }
        }

        return sb.ToString();
    }

    private List<ISymbol> GetFieldsAndPropertiesThatInitializedWithCreateMethod(ITypeSymbol typeSymbol)
    {
        var fields = new List<ISymbol>();
        var suffix = "_";
        var domainFields = typeSymbol.GetMembers().OfType<IFieldSymbol>().ToList();
        var domainProperties = typeSymbol.GetMembers().OfType<IPropertySymbol>().ToList();

        var createMethod = GetCreteMethod(typeSymbol);
        if (createMethod is null) return new List<ISymbol>();
        var createParameters = createMethod.Parameters;

        foreach (var domainField in domainFields)
        {
            if (domainField.Name.ToLower().Contains("id"))
            {
                if (createParameters.Any(cp => cp.Name.ToLower() == domainField.Name.ToLower()))
                {
                    fields.Add(domainField);
                }
            }
            else
            {
                if (createParameters.Any(cp => suffix + cp.Name.ToLower() == domainField.Name.ToLower()))
                {
                    fields.Add(domainField);
                }
            }
        }

        foreach (var domainProperty in domainProperties)
        {
            if (domainProperty.Name.ToLower().Contains("id"))
            {
                if (createParameters.Any(cp =>
                        string.Equals(cp.Name, domainProperty.Name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    fields.Add(domainProperty);
                }
            }
        }

        return fields;
    }

    private static IMethodSymbol GetCreteMethod(ITypeSymbol typeSymbol)
    {
        return typeSymbol.GetMembers().OfType<IMethodSymbol>()
            .FirstOrDefault(m =>
                m.IsStatic && m.DeclaredAccessibility == Accessibility.Public && m.Parameters.Length > 0 &&
                m.Name == "Create");
    }

    private string GenerateBackingFieldsAndIdProperties(ITypeSymbol typeSymbol)
    {
        var sb = new StringBuilder();

        foreach (var propertySymbol in typeSymbol.GetMembers().OfType<IPropertySymbol>()
                     .Where(x => x.Name.ToLower().Contains("id")))
        {
            sb.AppendLine(propertySymbol.Type.Name == nameof(Guid)
                ? $@"
    private {propertySymbol.Type} {propertySymbol.Name} = Guid.NewGuid();"
                : $@"
    private {propertySymbol.Type} {propertySymbol.Name} = default;");
        }

        var suffix = "_";
        foreach (var fieldSymbol in typeSymbol.GetMembers().OfType<IFieldSymbol>()
                     .Where(x => x.Name.StartsWith(suffix)))
        {
            sb.AppendLine(fieldSymbol.Type.Name == nameof(String)
                ? $@"
    private {fieldSymbol.Type} {fieldSymbol.Name} = ""{Guid.NewGuid().ToString()[..10]}"";"
                : $@"
    private {fieldSymbol.Type} {fieldSymbol.Name} = default;");
        }
        return sb.ToString();
    }
}

