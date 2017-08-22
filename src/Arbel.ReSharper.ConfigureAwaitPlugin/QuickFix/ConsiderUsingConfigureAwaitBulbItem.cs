using JetBrains.Application.Progress;
using JetBrains.Application.Threading;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
using JetBrains.ReSharper.Psi.CSharp.ConstantValues;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix
{
    public sealed class ConsiderUsingConfigureAwaitBulbItem : IBulbAction
    {
        private readonly IAwaitExpression _literalExpression;
        private readonly bool _value;

        public ConsiderUsingConfigureAwaitBulbItem(IAwaitExpression literalExpression, bool value)
        {
            _literalExpression = literalExpression;
            _value = value;
        }

        public string Text => $"Add 'ConfigureAwait({GetValueText()})'";

        private string GetValueText()
        {
            return _value ? "true" : "false";
        }

        public void Execute(ISolution solution, ITextControl textControl)
        {
            if (!_literalExpression.IsValid())
                return;

            var containingFile = _literalExpression.GetContainingFile();
            var psiModule = _literalExpression.GetPsiModule();
            var elementFactory = CSharpElementFactory.GetInstance(_literalExpression);
            
            IExpression newExpression = null;
            _literalExpression.GetPsiServices().Transactions.Execute(GetType().Name, () =>
            {
                using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                    newExpression = ModificationUtil.ReplaceChild(
                      _literalExpression.Task, elementFactory.CreateExpression("$0.ConfigureAwait($1)", _literalExpression.Task, 
                        elementFactory.CreateExpressionByConstantValue(CSharpConstantValueFactory.CreateBoolValue(_value, psiModule))));
            });

            if (newExpression != null)
            {
                IRangeMarker marker = newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
                containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Create());
            }
        }
    }
}