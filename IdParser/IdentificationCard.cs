﻿using System;
using System.Collections.Generic;

namespace IdParser;

public class IdentificationCard
{
    public IssuerIdentificationNumber IssuerIdentificationNumber { get; set; }
    public Version AamvaVersionNumber { get; set; }
    public byte JurisdictionVersionNumber { get; set; }
    public string IdNumber { get; set; }

    public Name Name { get; set; } = new Name();
    public Address Address { get; set; } = new Address();

    public DateTime DateOfBirth { get; set; }
    public DateTime? Under18Until { get; set; }
    public DateTime? Under19Until { get; set; }
    public DateTime? Under21Until { get; set; }

    public DateTime ExpirationDate { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? RevisionDate { get; set; }

    public Sex Sex { get; set; }
    public EyeColor? EyeColor { get; set; }
    public HairColor? HairColor { get; set; }
    public Ethnicity? Ethnicity { get; set; }
    public Height Height { get; set; }
    public Weight Weight { get; set; }
    
    public string DocumentDiscriminator { get; set; }
    
    public string PlaceOfBirth { get; set; }
    public string AuditInformation { get; set; }
    public string InventoryControlNumber { get; set; }
    
    public ComplianceType? ComplianceType { get; set; }

    public bool? HasTemporaryLawfulStatus { get; set; }
    public bool? IsOrganDonor { get; set; }
    public bool? IsVeteran { get; set; }

    public Dictionary<string, string> AdditionalJurisdictionElements { get; } = new Dictionary<string, string>();
}
