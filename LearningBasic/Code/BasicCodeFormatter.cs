﻿namespace LearningBasic.Code
{
    using System;

    public class BasicCodeFormatter : ICodeFormatter<Tag>
    {
        public string Format(AstNode<Tag> statement)
        {
            return string.Empty;
        }
    }
}