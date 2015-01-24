﻿using OfficeDevPnP.PowerShell.Commands.Base;
using OfficeDevPnP.PowerShell.Commands.Base.PipeBinds;
using Microsoft.SharePoint.Client;
using System;
using System.Management.Automation;

namespace OfficeDevPnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SPOPropertyBag")]
    public class GetPropertyBag : SPOWebCmdlet
    {
        [Parameter(Mandatory = false, Position=0, ValueFromPipeline=true)]
        public string Key = string.Empty;
        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Key))
            {
                WriteObject(SelectedWeb.GetPropertyBagValueString(Key, string.Empty));
            }
            else
            {
                if (SelectedWeb.IsPropertyAvailable("AllProperties"))
                {
                    WriteObject(SelectedWeb.AllProperties.FieldValues);
                }
                else
                {
                    var values = SelectedWeb.AllProperties;
                    ClientContext.Load(values);
                    ClientContext.ExecuteQuery();
                    WriteObject(values.FieldValues);
                }
            }
        }
    }
}