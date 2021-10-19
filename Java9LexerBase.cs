/*
 * [The "BSD license"]
 *  Copyright (c) 2014 Terence Parr
 *  Copyright (c) 2014 Sam Harwell
 *  Copyright (c) 2017 Chan Chung Kwong
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 *  IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 *  OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 *  IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 *  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 *  THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;

namespace JavaPlantUmlGenerator
{
    public abstract class Java9LexerBase : Lexer
    {
        private readonly ICharStream _input;

        public override string[] RuleNames => throw new NotImplementedException();

        public override IVocabulary Vocabulary => throw new NotImplementedException();

        public override IDictionary<string, int> TokenTypeMap => base.TokenTypeMap;

        public override IDictionary<string, int> RuleIndexMap => base.RuleIndexMap;

        public override string SerializedAtn => base.SerializedAtn;

        public override string GrammarFileName => throw new NotImplementedException();

        public override ATN Atn => base.Atn;

        public override LexerATNSimulator Interpreter { get => base.Interpreter; protected set => base.Interpreter = value; }

        public override ParseInfo ParseInfo => base.ParseInfo;

        public override IList<IAntlrErrorListener<int>> ErrorListeners => base.ErrorListeners;

        public override IAntlrErrorListener<int> ErrorListenerDispatch => base.ErrorListenerDispatch;

        public override ITokenFactory TokenFactory { get => base.TokenFactory; set => base.TokenFactory = value; }

        public override string SourceName => base.SourceName;

        public override IIntStream InputStream => base.InputStream;

        public override int Line { get => base.Line; set => base.Line = value; }
        public override int Column { get => base.Column; set => base.Column = value; }

        public override int CharIndex => base.CharIndex;

        public override int TokenStartCharIndex => base.TokenStartCharIndex;

        public override int TokenStartLine => base.TokenStartLine;

        public override int TokenStartColumn => base.TokenStartColumn;

        public override string Text { get => base.Text; set => base.Text = value; }
        public override IToken Token { get => base.Token; set => base.Token = value; }
        public override int Type { get => base.Type; set => base.Type = value; }
        public override int Channel { get => base.Channel; set => base.Channel = value; }

        public override Stack<int> ModeStack => base.ModeStack;

        public override int CurrentMode { get => base.CurrentMode; set => base.CurrentMode = value; }
        public override bool HitEOF { get => base.HitEOF; set => base.HitEOF = value; }

        public override string[] ChannelNames => base.ChannelNames;

        public override string[] ModeNames => base.ModeNames;

        protected Java9LexerBase(ICharStream input, TextWriter output, TextWriter errorOutput)
            : base(input, output, errorOutput)
        {
            _input = input;
        }

        private class Character
        {
            public static bool IsJavaIdentifierPart(int c)
            {
                if (Char.IsLetter((char)c))
                    return true;
                else if (c == (int)'$')
                    return true;
                else if (c == (int)'_')
                    return true;
                else if (Char.IsDigit((char)c))
                    return true;
                else if (Char.IsNumber((char)c))
                    return true;
                return false;
            }

            public static bool IsJavaIdentifierStart(int c)
            {
                if (Char.IsLetter((char)c))
                    return true;
                else if (c == (int)'$')
                    return true;
                else if (c == (int)'_')
                    return true;
                return false;
            }

            public static int ToCodePoint(int high, int low)
            {
                return Char.ConvertToUtf32((char)high, (char)low);
            }
        }

        public bool Check1()
        {
            return Character.IsJavaIdentifierStart(_input.LA(-1));
        }

        public bool Check2()
        {
            return Character.IsJavaIdentifierStart(Character.ToCodePoint((char)_input.LA(-2), (char)_input.LA(-1)));
        }

        public bool Check3()
        {
            return Character.IsJavaIdentifierPart(_input.LA(-1));
        }

        public bool Check4()
        {
            return Character.IsJavaIdentifierPart(Character.ToCodePoint((char)_input.LA(-2), (char)_input.LA(-1)));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override IDictionary<string, int> CreateTokenTypeMap(IVocabulary vocabulary)
        {
            return base.CreateTokenTypeMap(vocabulary);
        }

        public override int GetTokenType(string tokenName)
        {
            return base.GetTokenType(tokenName);
        }

        [return: NotNull]
        public override string GetErrorHeader(RecognitionException e)
        {
            return base.GetErrorHeader(e);
        }

        [Obsolete("dunno why")]
        public override string GetTokenErrorDisplay(IToken t)
        {
            return base.GetTokenErrorDisplay(t);
        }

        public override void AddErrorListener(IAntlrErrorListener<int> listener)
        {
            base.AddErrorListener(listener);
        }

        public override void RemoveErrorListener(IAntlrErrorListener<int> listener)
        {
            base.RemoveErrorListener(listener);
        }

        public override void RemoveErrorListeners()
        {
            base.RemoveErrorListeners();
        }

        public override bool Sempred(RuleContext _localctx, int ruleIndex, int actionIndex)
        {
            return base.Sempred(_localctx, ruleIndex, actionIndex);
        }

        public override bool Precpred(RuleContext localctx, int precedence)
        {
            return base.Precpred(localctx, precedence);
        }

        public override void Action(RuleContext _localctx, int ruleIndex, int actionIndex)
        {
            base.Action(_localctx, ruleIndex, actionIndex);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override IToken NextToken()
        {
            return base.NextToken();
        }

        public override void Skip()
        {
            base.Skip();
        }

        public override void More()
        {
            base.More();
        }

        public override void Mode(int m)
        {
            base.Mode(m);
        }

        public override void PushMode(int m)
        {
            base.PushMode(m);
        }

        public override int PopMode()
        {
            return base.PopMode();
        }

        public override void SetInputStream(ICharStream input)
        {
            base.SetInputStream(input);
        }

        public override void Emit(IToken token)
        {
            base.Emit(token);
        }

        public override IToken Emit()
        {
            return base.Emit();
        }

        public override IToken EmitEOF()
        {
            return base.EmitEOF();
        }

        public override IList<IToken> GetAllTokens()
        {
            return base.GetAllTokens();
        }

        public override void Recover(LexerNoViableAltException e)
        {
            base.Recover(e);
        }

        public override void NotifyListeners(LexerNoViableAltException e)
        {
            base.NotifyListeners(e);
        }

        public override string GetErrorDisplay(string s)
        {
            return base.GetErrorDisplay(s);
        }

        public override string GetErrorDisplay(int c)
        {
            return base.GetErrorDisplay(c);
        }

        public override string GetCharErrorDisplay(int c)
        {
            return base.GetCharErrorDisplay(c);
        }

        public override void Recover(RecognitionException re)
        {
            base.Recover(re);
        }
    }
}