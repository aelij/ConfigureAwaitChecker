using System;
using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.Application.Threading;
using JetBrains.DocumentManagers;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Intentions.Scoped.Executors;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Feature.Services.QuickFixes.Scoped;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
using JetBrains.ReSharper.Psi.CSharp.ConstantValues;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix
{
    public abstract class ConsiderUsingConfigureAwaitQuickFixBase : ScopedQuickFixBase
    {
        private readonly ConsiderUsingConfigureAwaitHighlighting _highlighting;
        private readonly bool _value;

        protected ConsiderUsingConfigureAwaitQuickFixBase([NotNull] ConsiderUsingConfigureAwaitHighlighting highlighting, bool value)
        {
            _highlighting = highlighting;
            _value = value;
        }

        public override string Text => $"Add 'ConfigureAwait({GetValueText()})'";

        protected override IScopedFixingStrategy GetScopedFixingStrategy(ISolution solution)
        {
            return new SameQuickFixTypeStrategy(this, solution);
        }

        protected override ScopedActionExecutor GetScopedQuickFixExecutor(ISolution solution, IScopedFixingStrategy fixingStrategy,
            PsiLanguageType languageType)
        {
            return new ScopedQuickFixExecutor(solution, fixingStrategy, null, languageType);
        }
        
        public override bool IsAvailable(IUserDataHolder cache)
        {
            return _highlighting.IsValid();
        }

        private string GetValueText()
        {
            return _value ? "true" : "false";
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var literalExpression = _highlighting.Expression;
            if (!literalExpression.IsValid())
            {
                return null;
            }

            var containingFile = literalExpression.GetContainingFile();
            var psiModule = literalExpression.GetPsiModule();
            var elementFactory = CSharpElementFactory.GetInstance(literalExpression);

            IExpression newExpression = null;
            literalExpression.GetPsiServices().Transactions.Execute(GetType().Name, () =>
            {
                using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                    newExpression = ModificationUtil.ReplaceChild(
                        literalExpression.Task, elementFactory.CreateExpression("$0.ConfigureAwait($1)", literalExpression.Task,
                            elementFactory.CreateExpressionByConstantValue(CSharpConstantValueFactory.CreateBoolValue(_value, psiModule))));
            });

            if (newExpression != null)
            {
                var marker = newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
                containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Create());
            }

            return null;
        }

        protected override ITreeNode TryGetContextTreeNode()
        {
            return null;
        }
    }

    [QuickFix]
    public sealed class ConsiderUsingConfigureAwaitFalseQuickFix : ConsiderUsingConfigureAwaitQuickFixBase
    {
        public ConsiderUsingConfigureAwaitFalseQuickFix([NotNull] ConsiderUsingConfigureAwaitHighlighting highlighting) : base(highlighting, false)
        {
        }
    }

    [QuickFix]
    public sealed class ConsiderUsingConfigureAwaitTrueQuickFix : ConsiderUsingConfigureAwaitQuickFixBase
    {
        public ConsiderUsingConfigureAwaitTrueQuickFix([NotNull] ConsiderUsingConfigureAwaitHighlighting highlighting) : base(highlighting, true)
        {
        }
    }
}