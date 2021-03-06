﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information

namespace DotNetNuke.Entities.Portals
{
    using System;
    using System.Data;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    using DotNetNuke.Abstractions.Portals;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Urls;

    using NewBrowserType = DotNetNuke.Abstractions.Urls.BrowserTypes;

    /// <inheritdoc />
    [Serializable]
    public class PortalAliasInfo : BaseEntityInfo, IHydratable, IXmlSerializable, IPortalAliasInfo
    {
        public PortalAliasInfo()
        {
        }

        public PortalAliasInfo(PortalAliasInfo alias)
        {
            this.HttpAlias = alias.HttpAlias;
            this.PortalAliasId = alias.PortalAliasId;
            this.PortalId = alias.PortalId;
            this.IsPrimary = alias.IsPrimary;
            this.Redirect = alias.Redirect;
            ((IPortalAliasInfo)this).BrowserType = ((IPortalAliasInfo)alias).BrowserType;
            this.CultureCode = alias.CultureCode;
            this.Skin = alias.Skin;
        }

        /// <inheritdoc />
        public string HttpAlias { get; set; }

        [Obsolete("Deprecated in 9.7.2. Scheduled for removal in v11.0.0, use DotNetNuke.Abstractions.Portals.IPortalAliasInfo.HttpAlias instead.")]
        public string HTTPAlias
        {
            get => this.HttpAlias;
            set => this.HttpAlias = value;
        }

        /// <inheritdoc />
        public int PortalAliasId { get; set; }

        [Obsolete("Deprecated in 9.7.2. Scheduled for removal in v11.0.0, use DotNetNuke.Abstractions.Portals.IPortalAliasInfo.PortalAliasId instead.")]
        public int PortalAliasID
        {
            get => this.PortalAliasId;
            set => this.PortalAliasId = value;
        }

        /// <inheritdoc />
        public int PortalId { get; set; }

        [Obsolete("Deprecated in 9.7.2. Scheduled for removal in v11.0.0, use DotNetNuke.Abstractions.Portals.IPortalAliasInfo.PortalId instead.")]
        public int PortalID
        {
            get => this.PortalId;
            set => this.PortalId = value;
        }

        /// <inheritdoc />
        public bool IsPrimary { get; set; }

        /// <inheritdoc />
        public bool Redirect { get; set; }

        /// <summary>
        /// Gets or sets the Browser Type.
        /// </summary>
        [Obsolete("Deprecated in 9.7.2. Scheduled for removal in v11.0.0, use DotNetNuke.Abstractions.Portals.IPortalAliasInfo.BrowserType instead.")]
        public BrowserTypes BrowserType
        {
            get => (BrowserTypes)((IPortalAliasInfo)this).BrowserType;
            set => ((IPortalAliasInfo)this).BrowserType = (NewBrowserType)value;
        }

        /// <inheritdoc />
        NewBrowserType IPortalAliasInfo.BrowserType { get; set; }

        /// <inheritdoc />
        public string CultureCode { get; set; }

        /// <inheritdoc />
        public string Skin { get; set; }

        /// <inheritdoc />
        public int KeyID
        {
            get { return this.PortalAliasId; }
            set { this.PortalAliasId = value; }
        }

        public void Fill(IDataReader dr)
        {
            this.FillInternal(dr);

            this.PortalAliasId = Null.SetNullInteger(dr["PortalAliasID"]);
            this.PortalId = Null.SetNullInteger(dr["PortalID"]);
            this.HttpAlias = Null.SetNullString(dr["HTTPAlias"]);
            this.IsPrimary = Null.SetNullBoolean(dr["IsPrimary"]);
            var browserType = Null.SetNullString(dr["BrowserType"]);
            this.BrowserType = string.IsNullOrEmpty(browserType) || browserType.Equals("normal", StringComparison.OrdinalIgnoreCase)
                              ? BrowserTypes.Normal
                              : BrowserTypes.Mobile;
            this.CultureCode = Null.SetNullString(dr["CultureCode"]);
            this.Skin = Null.SetNullString(dr["Skin"]);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }

                if (reader.NodeType == XmlNodeType.Whitespace)
                {
                    continue;
                }

                switch (reader.Name)
                {
                    case "portalAlias":
                        break;
                    case "portalID":
                        this.PortalId = reader.ReadElementContentAsInt();
                        break;
                    case "portalAliasID":
                        this.PortalAliasId = reader.ReadElementContentAsInt();
                        break;
                    case "HTTPAlias":
                        this.HttpAlias = reader.ReadElementContentAsString();
                        break;
                    case "skin":
                        this.Skin = reader.ReadElementContentAsString();
                        break;
                    case "cultureCode":
                        this.CultureCode = reader.ReadElementContentAsString();
                        break;
                    case "browserType":
                        string type = reader.ReadElementContentAsString();
                        this.BrowserType = type.Equals("mobile", StringComparison.InvariantCultureIgnoreCase) ? BrowserTypes.Mobile : BrowserTypes.Normal;
                        break;
                    case "primary":
                        this.IsPrimary = reader.ReadElementContentAsBoolean();
                        break;
                    default:
                        if (reader.NodeType == XmlNodeType.Element && !string.IsNullOrEmpty(reader.Name))
                        {
                            reader.ReadElementContentAsString();
                        }

                        break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            // Write start of main elemenst
            writer.WriteStartElement("portalAlias");

            // write out properties
            writer.WriteElementString("portalID", this.PortalId.ToString());
            writer.WriteElementString("portalAliasID", this.PortalAliasId.ToString());
            writer.WriteElementString("HTTPAlias", this.HttpAlias);
            writer.WriteElementString("skin", this.Skin);
            writer.WriteElementString("cultureCode", this.CultureCode);
            writer.WriteElementString("browserType", this.BrowserType.ToString().ToLowerInvariant());
            writer.WriteElementString("primary", this.IsPrimary.ToString().ToLowerInvariant());

            // Write end of main element
            writer.WriteEndElement();
        }
    }
}
