namespace his.Models.Dto
{
    public class PatientDto
    {
        public int MEPatientID { get; set; } // MEPatientID (Primary key)
        public string AAStatus { get; set; } // AAStatus (length: 50)
        public string AACreatedUser { get; set; } // AACreatedUser (length: 50)
        public System.DateTime AACreatedDate { get; set; } // AACreatedDate
        public string AAUpdatedUser { get; set; } // AAUpdatedUser (length: 50)
        public System.DateTime AAUpdatedDate { get; set; } // AAUpdatedDate
        public int FK_MECompanyID { get; set; } // FK_MECompanyID
        public int FK_ARPriceLevelID { get; set; } // FK_ARPriceLevelID
        public int FK_MEOccupationID { get; set; } // FK_MEOccupationID
        public int FK_GELocationID { get; set; } // FK_GELocationID
        public string MEPatientNo { get; set; } // MEPatientNo (length: 50)
        public string MEPatientName { get; set; } // MEPatientName (length: 50)
        public string MEPatientType { get; set; } // MEPatientType (length: 50)
        public string MEPatientDesc { get; set; } // MEPatientDesc (length: 512)
        public string MEPatientTitle { get; set; } // MEPatientTitle (length: 20)
        public string MEPatientFirstName { get; set; } // MEPatientFirstName (length: 50)
        public string MEPatientMiddleName { get; set; } // MEPatientMiddleName (length: 50)
        public string MEPatientLastName { get; set; } // MEPatientLastName (length: 50)

        public string MEPatientBirthday { get; set; } // MEPatientBirthday
        public string MEGender { get; set; } // MEGender (length: 50)
        public bool MEPatientActiveCheck { get; set; } // MEPatientActiveCheck
        public string MEPatientOccupation { get; set; } // MEPatientOccupation (length: 50)
        public string MEMarital { get; set; } // MEMarital (length: 50)
        public string MEPatientEthnicity { get; set; } // MEPatientEthnicity (length: 50)
        public byte[] MEPatientPicture { get; set; } // MEPatientPicture
        public bool MEPatientHealthInsCheck { get; set; } // MEPatientHealthInsCheck
        public string MEPatientContactEmail { get; set; } // MEPatientContactEmail (length: 100)
        public string MEPatientContactPhone { get; set; } // MEPatientContactPhone (length: 50)
        public string MEPatientContactCellPhone { get; set; } // MEPatientContactCellPhone (length: 50)
        public string MEPatientContactFax { get; set; } // MEPatientContactFax (length: 50)
        public string MEPatientContactCompany { get; set; } // MEPatientContactCompany (length: 50)
        public string MEPatientContactDepartment { get; set; } // MEPatientContactDepartment (length: 50)
        public string MEPatientContactRoom { get; set; } // MEPatientContactRoom (length: 30)
        public string MEPatientContactAddressStreet { get; set; } // MEPatientContactAddressStreet (length: 200)
        public string MEPatientContactAddressLine1 { get; set; } // MEPatientContactAddressLine1 (length: 200)
        public string MEPatientContactAddressLine2 { get; set; } // MEPatientContactAddressLine2 (length: 200)
        public string MEPatientContactAddressLine3 { get; set; } // MEPatientContactAddressLine3 (length: 200)
        public string MEPatientContactAddressWard { get; set; } // MEPatientContactAddressWard (length: 50)
        public string MEPatientContactAddressDistrict { get; set; } // MEPatientContactAddressDistrict (length: 50)
        public string MEPatientContactAddressCity { get; set; } // MEPatientContactAddressCity (length: 50)
        public string MEPatientContactAddressPostalCode { get; set; } // MEPatientContactAddressPostalCode (length: 50)
        public string MEPatientContactAddressStateProvince { get; set; } // MEPatientContactAddressStateProvince (length: 50)
        public string MEPatientContactAddressCountry { get; set; } // MEPatientContactAddressCountry (length: 50)
        public string MEPatientMatchCode01Combo { get; set; } // MEPatientMatchCode01Combo (length: 50)
        public string MEPatientMatchCode02Combo { get; set; } // MEPatientMatchCode02Combo (length: 50)
        public string MEPatientMatchCode03Combo { get; set; } // MEPatientMatchCode03Combo (length: 50)
        public string MEPatientMatchCode04Combo { get; set; } // MEPatientMatchCode04Combo (length: 50)
        public string MEPatientMatchCode05Combo { get; set; } // MEPatientMatchCode05Combo (length: 50)
        public string MEPatientMatchCode06Combo { get; set; } // MEPatientMatchCode06Combo (length: 50)
        public string MEPatientBarcode { get; set; } // MEPatientBarcode (length: 50)
        public int FK_MEEthnicID { get; set; } // FK_MEEthnicID
        public string MEPatientContactCellPhone2 { get; set; } // MEPatientContactCellPhone2 (length: 50)
        public string MEPatientIDCard { get; set; } // MEPatientIDCard (length: 50)

        public string MEPatientIDCardDate { get; set; } // MEPatientIDCardDate
        public string MEPatientIDCardStateProvinces { get; set; } // MEPatientIDCardStateProvinces (length: 512)
        public string MEPatientPermanentResidence { get; set; } // MEPatientPermanentResidence (length: 200)
        public string MEPatientBs24x7ID { get; set; } // MEPatientBs24x7ID (length: 50)
        public int FK_ADUserGroupID { get; set; } // FK_ADUserGroupID
        public string MEPatientNoExternalHis { get; set; } // MEPatientNoExternalHis (length: 100)
        public string MEPatientRemoteCaseID { get; set; } // MEPatientRemoteCaseID (length: 100)

        public bool MEPatientBirthdayOnlyYear { get; set; }
    }
}
