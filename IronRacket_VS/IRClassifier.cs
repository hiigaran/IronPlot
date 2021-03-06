﻿// Copyright (c) Microsoft Corporati
// All rights reserved

namespace IRLanguage
{
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Adornments;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(ITaggerProvider))]
    [ContentType("IronRacket")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class IRClassifierProvider : ITaggerProvider
    {

        [Export]
        [Name("IronRacket")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition IRContentType = null;

        [Export]
        [FileExtension(".plot")]
        [ContentType("IronRacket")]
        internal static FileExtensionToContentTypeDefinition IRFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<IRTokenTag> IRTagAggregator = 
                                            aggregatorFactory.CreateTagAggregator<IRTokenTag>(buffer);

            return new IRClassifier(buffer, IRTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }

    internal sealed class IRClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<IRTokenTag> _aggregator;
        IDictionary<IRTokenTypes, IClassificationType> _IRTypes;

        internal IRClassifier(ITextBuffer buffer, 
                               ITagAggregator<IRTokenTag> IRTagAggregator, 
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = IRTagAggregator;
            _IRTypes = new Dictionary<IRTokenTypes, IClassificationType>();
            _IRTypes[IRTokenTypes.IRDefine] = typeService.GetClassificationType("define");
            _IRTypes[IRTokenTypes.IRCar] = typeService.GetClassificationType("car");
            _IRTypes[IRTokenTypes.IRCdr] = typeService.GetClassificationType("cdr");
            _IRTypes[IRTokenTypes.IRComment] = typeService.GetClassificationType("IRcomment");
            _IRTypes[IRTokenTypes.IRLambda] = typeService.GetClassificationType("lambda");
            _IRTypes[IRTokenTypes.IRCons] = typeService.GetClassificationType("cons");
            _IRTypes[IRTokenTypes.IRCond] = typeService.GetClassificationType("cond");
            _IRTypes[IRTokenTypes.IRIf] = typeService.GetClassificationType("if");
            _IRTypes[IRTokenTypes.IRLet] = typeService.GetClassificationType("let");
            _IRTypes[IRTokenTypes.IRLetrec] = typeService.GetClassificationType("letrec");
            _IRTypes[IRTokenTypes.IROr] = typeService.GetClassificationType("or");
            _IRTypes[IRTokenTypes.IRAnd] = typeService.GetClassificationType("and");
            _IRTypes[IRTokenTypes.IRSetbang] = typeService.GetClassificationType("set!");
            _IRTypes[IRTokenTypes.IRDefinevar] = typeService.GetClassificationType("define_var");
            _IRTypes[IRTokenTypes.IRNew] = typeService.GetClassificationType("new");
            _IRTypes[IRTokenTypes.IRWhile] = typeService.GetClassificationType("while");
            _IRTypes[IRTokenTypes.IRBegin] = typeService.GetClassificationType("begin");
            _IRTypes[IRTokenTypes.IRNullhuh] = typeService.GetClassificationType("null?");
            _IRTypes[IRTokenTypes.IRMap] = typeService.GetClassificationType("map");
            _IRTypes[IRTokenTypes.IREqualhuh] = typeService.GetClassificationType("equal?");
            _IRTypes[IRTokenTypes.IRNot] = typeService.GetClassificationType("not");
            _IRTypes[IRTokenTypes.IRCall] = typeService.GetClassificationType("call");
            _IRTypes[IRTokenTypes.IRScall] = typeService.GetClassificationType("scall");
            _IRTypes[IRTokenTypes.IRDisplayln] = typeService.GetClassificationType("displayln");
            _IRTypes[IRTokenTypes.IRBool] = typeService.GetClassificationType("bool");
            _IRTypes[IRTokenTypes.IRUsing] = typeService.GetClassificationType("using");

            
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (var tagSpan in this._aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return 
                    new TagSpan<ClassificationTag>(tagSpans[0], 
                                                   new ClassificationTag(_IRTypes[tagSpan.Tag.type]));
            }
        }
    }
}
