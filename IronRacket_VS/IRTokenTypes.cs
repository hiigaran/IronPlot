﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRLanguage
{
    public enum IRTokenTypes
    {
       IRDefine, IRCdr, IRCar, IRComment, IRLambda, IRCons, IRSetbang, IRIf, IRAnd, IROr, IRCond, IRLet, IRLetrec, IRDefinevar, 
        IRString, IRNew, IRWhile, IRBegin, IRNullhuh, IRMap, IREqualhuh, IRNot, IRCall, IRScall, IRDisplayln, IRBool, IRUsing
       //Keyword includes while 
    }
}


