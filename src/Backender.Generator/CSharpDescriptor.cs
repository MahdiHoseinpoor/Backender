using Backender.Translator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Backender.Translator.Models.File;

namespace Backender.Generator
{
    public static class CSharpDescriptor
    {
        public static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(this File file)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(file.BodyContext);
            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

            var constructors = root.DescendantNodes()
                                   .OfType<ConstructorDeclarationSyntax>();
            return constructors;
        }
        //private bool IsCSharpCode(string sourceCode)
        //{
        //    SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        //    var diagnostics = syntaxTree.GetDiagnostics();
        //    return !diagnostics.Any();
        //}
    }
}