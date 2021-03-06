﻿using System;
using System.Data.Entity;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database.Context;

namespace InsuranceV2.Infrastructure.Database.Initializer
{
    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<InsuranceAppContext>
    {
        protected override void Seed(InsuranceAppContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                var insuree = new Insuree
                {
                    FirstName = "FirstName" + i,
                    LastName = "LastName" + i,
                    DateOfBirth = DateTime.Now.AddYears(-30 + i)
                };

                insuree.Addresses.Add("street" + i, "123", "12345", "city" + i, "country" + i, ContactType.Personal);
                insuree.Addresses.Add("street" + i, "123", "12345", "city" + i, "country" + i, ContactType.Business);
                insuree.Addresses.Add("street" + i, "123", "12345", "city" + i, "country" + i, ContactType.Partner);

                insuree.EmailAddresses.Add("first" + i + "@test.com", ContactType.Personal);
                insuree.EmailAddresses.Add("second" + i + "@test.com", ContactType.Business);

                insuree.PhoneNumbers.Add("1234567890" + i, PhoneType.Phone, ContactType.Personal);
                insuree.PhoneNumbers.Add("1234567890" + i, PhoneType.Mobile, ContactType.Partner);
                insuree.PhoneNumbers.Add("1234567890" + i, PhoneType.Fax, ContactType.Business);

                insuree.DateOfMarriage = DateTime.Today;

                insuree.Partner = new Insuree
                {
                    FirstName = "PartnerFirstName" + i,
                    LastName = "PartnerLastName" + i,
                    DateOfBirth = DateTime.Now.AddYears(-30 + i)
                };

                for (var j = 0; j < 2; j++)
                {
                    var insurance = new Insurance
                    {
                        InsuranceNumber = "ABC " + j,
                        Cancelled = j == 0,
                        Company = InsuranceCompany.Vhv,
                        Type = j == 0 ? InsuranceType.Php : InsuranceType.Kfz,
                        ContractDate = DateTime.Now.AddDays(j * 10),
                        LicensePlate = j == 0 ? "" : "AB-ABC 123",
                        StartDate = DateTime.Now.AddDays(j * 5)
                    };

                    insuree.Insurances.Add(insurance);
                }

                context.Insurees.Add(insuree);
            }
        }
    }
}