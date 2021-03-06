﻿using System;
using System.Collections.Generic;

namespace DaCoder.Data
{
    /// <summary>
    /// Language entity.
    /// </summary>
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Keyword> Keywords { get; set; }

    }
}