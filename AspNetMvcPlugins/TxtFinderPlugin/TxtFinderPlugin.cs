﻿using Autofac;
using Common;

namespace TxtFinderPlugin
{
    public class TxtFinderPlugin : PluginModule
    {
        public override string ToString()
        {
            return ".txt searcher";
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Finder>().As<IFinder>();
        }
    }
}