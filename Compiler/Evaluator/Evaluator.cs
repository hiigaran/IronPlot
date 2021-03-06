﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Evaluator
{
    public class EvaluatorException : Exception
    {
        public EvaluatorException()
        {
        }

        public EvaluatorException(String message)
            : base(message)
        { }

        public EvaluatorException(String message, Exception inner)
            : base(message, inner)
        { }
    }

    public class Evaluator
    {
        Dictionary<string, Expr> env;
    //    Dictionary<string,Int64> env;
  //      Tuple<Dictionary<Int64, Expr>, Int64> store;

        public Evaluator()
        {
            env = new Dictionary<string,Expr>();
        }

        // external facing function
        public List<string> evaluate(string input)
        {
            List<Expr> exprs = parseTopLevel(input);
            List<string> ret = new List<string>();
            for (int i = 0; i < exprs.Count; i++)
            {
                Expr expr = exprs[i].eval(env);
                if (expr.GetType() == typeof(EnvExpr))
                    env = expr.eval(env);
                else
                    ret.Add(expr.ToString());
            }
            return ret;
        }
        private List<Expr> parseTopLevel(string input)
        {
            // if no opening paren, then just one expression and can call parse
            // if opening parent then there may be more than one expr
            // can just call parse List to get each expr
            List<Expr> exprs = new List<Expr>();
            string in_ = eatWhitespace(input);
            string fExpr;
            while (in_ != "")
            {
                if (peekChar(in_) == '(') // may be more than one
                    fExpr = peekList(in_);
                else
                    fExpr = peekWord(in_);

                exprs.Add(parse(fExpr));
                in_ = eatWhitespace(eatWord(fExpr, in_));
            }
            return exprs;
        }

        private Expr parse(string input)
        {
            string in_ = eatWhitespace(input);
            if (peekChar(in_) == '(')
            {
                in_ = eatWhitespace(eatChar('(', in_));
                Expr fun;
                List<Expr> args = new List<Expr>();
                string fun_string;
                if (peekChar(in_) == '(') 
                {
                    fun_string = peekList(in_);
                    fun = parse(fun_string);
                }
                else
                {
                    fun_string = peekWord(in_);
                    if (env.ContainsKey(fun_string))
                        fun = new VarExpr(fun_string);
                    else
                        switch (fun_string)
                        {
                            case "lambda":
                                    return parseLam(in_);
                            case "define":
                                    return parseDef(in_);
                            case "if": // if appears to be special too
                                    return parseIf(in_);
                            case "or":
                                    return parseOr(in_);
                            case "and":
                                    return parseAnd(in_);
                            default:
                                {
                                    fun = new VarExpr(fun_string);
                                    break;
                                }
                        }
                }

                in_ = eatWhitespace(eatWord(fun_string, in_));

                while (peekChar(in_) != ')')
                {
                    if (peekChar(in_) == '(')
                    {
                        string word = peekList(in_);
                        args.Add(parse(word));
                        in_ = eatWhitespace(eatWord(word, in_));
                    }
                    else
                    {
                        string word = peekWord(in_);
                        args.Add(parse(word));
                        in_ = eatWhitespace(eatWord(word, in_));
                    }
                }
                   return new AppExpr(fun, args, env); 
            }
            else
            {
                if (peekChar(in_) == '"') // we have a string
                    return new StrExpr(in_);

                long num;
                if (Int64.TryParse(input, out num))
                {
                    return new NumExpr(num);
                }
                else if (input == "empty")
                    return new EmptyExpr();
                else if (input == "#t" || input == "true")
                    return new BoolExpr(true);
                else if (input == "#f" || input == "false")
                    return new BoolExpr(false);
                else
                    return new VarExpr(input); 
            }
        }

        private AndExpr parseAnd(string input)
        {
            // first thing is and
            string in_ = eatWhitespace(eatWord("and", input));
            List<Expr> args = new List<Expr>();
            string arg_s;
            Expr arg;
            while (peekChar(in_) != ')')
            {
                if (peekChar(in_) == '(')
                    arg_s = peekList(in_);
                else if (peekChar(in_) == '"')
                {
                    arg_s = peekString(in_);
                }
                else
                    arg_s = peekWord(in_);

                arg = parse(arg_s);
                args.Add(arg);
                in_ = eatWhitespace(eatWord(arg_s, in_));
            }
            return new AndExpr(args);
        }

        private OrExpr parseOr(string input)
        {
            // first thing is or
            string in_ = eatWhitespace(eatWord("or", input));
            List<Expr> args = new List<Expr>();
            string arg_s;
            Expr arg;
            while (peekChar(in_) != ')')
            {
                if (peekChar(in_) == '(')
                    arg_s = peekList(in_);
                    else if (peekChar(in_) == '"')
                {
                    arg_s = peekString(in_);
                }
                else
                    arg_s = peekWord(in_);

                arg = parse(arg_s);
                in_ = eatWhitespace(eatWord(arg_s, in_));
            }
            return new OrExpr(args);
        }

        private IfExpr parseIf(string input)
        {
            // first thing is if
            string in_ = input;
            in_ = eatWhitespace(eatWord("if", in_));
            Expr cond;
            string cond_s;
            if (peekChar(in_) == '(')
                cond_s = peekList(in_);
            else if (peekChar(in_) == '"')
            {
                    cond_s = peekString(in_);
            }
            else
                cond_s = peekWord(in_);

            cond = parse(cond_s);
            in_ = eatWhitespace(eatWord(cond_s, in_));

            Expr then;
            string then_s;
            if (peekChar(in_) == '(')
                then_s = peekList(in_);
            else if (peekChar(in_) == '"')
            {
                then_s = peekString(in_);
            }
            else
                then_s = peekWord(in_);

            then = parse(then_s);
            in_ = eatWhitespace(eatWord(then_s, in_));

            Expr else_;
            string else_s;
            if (peekChar(in_) == '(')
                else_s = peekList(in_);
            else if (peekChar(in_) == '"')
            {
                else_s = peekString(in_);
            }
            else
                else_s = peekWord(in_);

            else_ = parse(else_s);
            in_ = eatWhitespace(eatWord(else_s, in_));

            if (peekChar(in_) != ')')
                throw new EvaluatorException("if bad syntax");
            return new IfExpr(cond, then, else_);
        }
        /*  parses a lambda
         */
        private LamExpr parseLam(string input)
        {
            // first thing is lambda
            string in_ = input;
            in_ = eatWhitespace(eatWord("lambda", in_));
            if (peekChar(in_) != '(')
                throw new EvaluatorException("lambda: bad syntax");

            in_ = eatWhitespace(eatChar('(', in_));

            // parse args
            List<string> args = new List<string>();
            string arg;
            while (peekChar(in_) != ')')
            {
                arg = peekWord(in_);
                args.Add(arg);
                in_ = eatWhitespace(eatWord(arg, in_));
            }
            in_ = eatWhitespace(eatChar(')', in_));
            if (peekChar(in_) == ')')
                throw new EvaluatorException("lambda: bad sytax");

            List<Expr> body = new List<Expr>();

            while (peekChar(in_) != ')')
            {
                string word;
                if (peekChar(in_) == '(')
                    word = peekList(in_);
                else if (peekChar(in_) == '"')
                {
                    word = peekString(in_);
                }
                else
                    word = peekWord(in_);   

                body.Add(parse(word));
                in_ = eatWhitespace(eatWord(word, in_));
            }

            return new LamExpr(args, body);
        }

        /* parses both defines
         * determining with type of define it is
         */
        private Expr parseDef(string input)
        {
            // first thing is define
            string in_ = eatWhitespace(eatWord("define", input));
            if (peekChar(in_) == '(') // FuncDefExpr
            {
                in_ = eatWhitespace(eatChar('(', in_));
                string name;
                List<string> args = new List<string>();
                List<Expr> body = new List<Expr>();
                if (peekChar(in_) == ')')
                    throw new EvaluatorException("define: bad syntax");
                name = peekWord(in_);
                in_ = eatWhitespace(eatWord(name, in_));

                while (peekChar(in_) != ')')
                {
                    if (peekChar(in_) == '(')
                        throw new EvaluatorException("define: bad syntax");
                    string arg = peekWord(in_);
                    args.Add(arg);
                    in_ = eatWhitespace(eatWord(arg, in_));
                }

                in_ = eatWhitespace(eatChar(')', in_));

                // parse body ... +
                if (peekChar(in_) == ')')
                    throw new EvaluatorException("define: bad syntax");

                while (peekChar(in_) != ')')
                {
                    string word;
                    if (peekChar(in_) == '(')
                        word = peekList(in_);
                    else if (peekChar(in_) == '"')
                    {
                        word = peekString(in_);
                    }
                    else
                        word = peekWord(in_);

                    body.Add(parse(word));
                    in_ = eatWhitespace(eatWord(word, in_));
                }

                return new FuncDefExpr(name, args, body);

            }
            else // AtomDefExpr
            {
                string id = peekWord(in_);
                in_ = eatWhitespace(eatWord(id, in_));

                string exp;
                if (peekChar(in_) == '(')
                    exp = peekList(in_);
                else if (peekChar(in_) == '"')
                {
                    exp = peekString(in_);
                }
                else
                    exp = peekWord(in_);

                Expr exp_ = parse(exp);
                in_ = eatWhitespace(eatWord(exp, in_));
                if (peekChar(in_) != ')')
                    throw new EvaluatorException("bad syntax: define");

                return new AtomDefExpr(id, exp_);
            }
        }

        // returns first char in string
        private char peekChar(string input)
        {
            return input[0];
        }

        /*
             returns string from beginning of input to first
             ' ' | '(' | ')' 
         * should call eatWhitespace first
         */
        private string peekWord(string input)
        {
            char c;
            int i;
            int len = input.Length;
            for (i = 0; i < len; i++)
            {
                c = input[i];
                if (c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == '(' || c == ')')
                    break;
            }
            return input.Substring(0, i);
        }

        private string peekList(string input)
        {
            if (input[0] != '(')
                throw new EvaluatorException("error peeking list");

            int i;
            int len = input.Length;
            int open_paren = 1;
            for (i = 1; i < len; i++)
            {
                if (input[i] == '(')
                    open_paren += 1;
                else if (input[i] == ')')
                    open_paren = open_paren - 1;

                if (open_paren == 0)
                    break;
            }
            if (open_paren != 0)
                throw new EvaluatorException("Paren mismatch");
            return input.Substring(0, i+1);
        }

        private string peekString(string input)
        {
            if (input[0] != '"')
                throw new EvaluatorException("error peeking string");

            int i;
            int len = input.Length;
            for (i = 1; i < len; i++)
            {
                if (input[i] == '"')
                    break;
            }
            return input.Substring(0, i + 1);

        }

        //returns new string, to be functional
        private string eatChar(char c, string input)
        {
            if (input[0] != c)
                throw new EvaluatorException("error eating char");
            
            return input.Substring(1);
        }

        private string eatWord(string s, string input)
        {
            if (input.Substring(0, s.Length) != s)
                throw new EvaluatorException("error eating word");

            return input.Substring(s.Length);
        }

        private string eatWhitespace(string input)
        {
            int i;
            int len = input.Length;
            char c;
            for (i = 0; i < len; i++)
            {
                c = input[i];
                if (c == ' ' || c == '\t' || c == '\n' || c == '\r')
                    continue;
                else
                    break;
            }
            return input.Substring(i);
        }
    }
}
