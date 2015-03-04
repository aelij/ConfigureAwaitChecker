using System.Collections.Generic;
using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
#if RS_V9
using JetBrains.ReSharper.Feature.Services.Intentions;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
#endif
#if RS_V8
using JetBrains.ReSharper.Intentions.Extensibility;
using JetBrains.ReSharper.Intentions.Extensibility.Menu;
#endif
using JetBrains.Util;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix
{
    [QuickFix]
    public sealed class ConsiderUsingConfigureAwaitQuickFix : IQuickFix
    {
        private readonly ConsiderUsingConfigureAwaitHighlighting _highlighting;

        public ConsiderUsingConfigureAwaitQuickFix([NotNull] ConsiderUsingConfigureAwaitHighlighting highlighting)
        {
            _highlighting = highlighting;
        }

        public IEnumerable<IntentionAction> CreateBulbItems()
        {
            return new[]
            {
                new ConsiderUsingConfigureAwaitBulbItem(_highlighting.Expression, false),
                new ConsiderUsingConfigureAwaitBulbItem(_highlighting.Expression, true),
            }.ToQuickFixAction();
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _highlighting.IsValid();
        }

    }
}